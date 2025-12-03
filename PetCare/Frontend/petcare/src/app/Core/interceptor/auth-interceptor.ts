import { HttpInterceptorFn, HttpErrorResponse, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, throwError, of } from 'rxjs';
import { catchError, switchMap, finalize, filter, take } from 'rxjs/operators';
import { Apicommuncation } from '../../Shared/api/apicommuncation';

let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const api = inject(Apicommuncation);
  const router = inject(Router);

  const token = localStorage.getItem('accessToken');
  const isRefreshRequest = req.url.includes('/api/auth/refresh');

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

      if (isRefreshRequest) {
        logoutAndRedirect();
        return throwError(() => error);
      }

      if (isRefreshing) {
        return refreshTokenSubject.pipe(
          filter(token => token !== null),
          take(1),
          switchMap((newToken) => {
            return newToken
              ? next(req.clone({ setHeaders: { Authorization: `Bearer ${newToken}` } }))
              : throwError(() => new Error('Session expired'));
          })
        );
      }

      isRefreshing = true;
      refreshTokenSubject.next(null); 

      return api.getRefershToken().pipe(
        switchMap((res) => {
          const newToken = res.accessToken;
          localStorage.setItem('accessToken', newToken);
          refreshTokenSubject.next(newToken);

          return next(req.clone({
            setHeaders: { Authorization: `Bearer ${newToken}` }
          }));
        }),
        catchError((refreshError) => {
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
    localStorage.removeItem('refreshToken'); 
    router.navigate(['/login'], { replaceUrl: true });
  }
};