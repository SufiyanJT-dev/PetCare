import { HttpInterceptorFn, HttpErrorResponse, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, throwError, of } from 'rxjs';
import { catchError, switchMap, finalize, filter, take } from 'rxjs/operators';
import { Apicommuncation } from '../../Shared/api/apicommuncation';

// Global refresh state
let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const api = inject(Apicommuncation);
  const router = inject(Router);

  const token = localStorage.getItem('accessToken');
  const isRefreshRequest = req.url.includes('/api/auth/refresh');

  // Add token if exists (except for refresh itself)
  let authReq = req;
  if (token && !isRefreshRequest) {
    authReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status !== 401) {
        return throwError(() => error);
      }

      // If refresh token endpoint itself fails → logout immediately
      if (isRefreshRequest) {
        logoutAndRedirect();
        return throwError(() => error);
      }

      // If already refreshing, queue the request
      if (isRefreshing) {
        return refreshTokenSubject.pipe(
          filter(token => token !== null),
          take(1),
          switchMap((newToken) => {
            // Retry with new token (or fail if null)
            return newToken
              ? next(req.clone({ setHeaders: { Authorization: `Bearer ${newToken}` } }))
              : throwError(() => new Error('Session expired'));
          })
        );
      }

      // First 401 → start refresh process
      isRefreshing = true;
      refreshTokenSubject.next(null); // Clear previous token

      return api.getRefershToken().pipe(
        switchMap((res) => {
          const newToken = res.accessToken;
          localStorage.setItem('accessToken', newToken);
          refreshTokenSubject.next(newToken);

          // Retry original request with new token
          return next(req.clone({
            setHeaders: { Authorization: `Bearer ${newToken}` }
          }));
        }),
        catchError((refreshError) => {
          // Refresh failed → logout
          logoutAndRedirect();
          refreshTokenSubject.next(null);
          return throwError(() => refreshError);
        }),
        finalize(() => {
          isRefreshing = false;
        })
      );
    })
  );

  function logoutAndRedirect() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken'); // if you store it
    router.navigate(['/login'], { replaceUrl: true });
  }
};