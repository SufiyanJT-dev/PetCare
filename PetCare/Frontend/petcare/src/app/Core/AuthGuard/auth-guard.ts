import { CanActivateFn, Router } from '@angular/router';
import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

export const authguardGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  const platformId = inject(PLATFORM_ID);

  let token: string | null = null;

  if (isPlatformBrowser(platformId)) {
    token = localStorage.getItem('accessToken');
  }

  if (token) {
    return true;
  } 
  else {
    router.navigate(['/sign-in']);
    return false;
  }
};
