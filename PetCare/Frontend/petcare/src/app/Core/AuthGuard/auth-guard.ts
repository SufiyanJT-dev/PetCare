import { CanActivateFn, Router } from '@angular/router';
import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

/**
 * AuthGuard to protect routes that require authentication.
 * Checks if an access token exists in localStorage (only on browser).
 */
export const authguardGuard: CanActivateFn = (route, state) => {
  // Inject Router for navigation
  const router = inject(Router);

  // Inject platform ID to check if code is running in browser
  const platformId = inject(PLATFORM_ID);

  // Initialize token as null
  let token: string | null = null;

  // Only access localStorage if running in a browser environment
  if (isPlatformBrowser(platformId)) {
    token = localStorage.getItem('accessToken');
  }

  // If token exists, allow route activation
  if (token) {
    return true;
  } 
  // Otherwise, redirect to sign-in page
  else {
    router.navigate(['/sign-in']);
    return false;
  }
};
