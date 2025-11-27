import { Routes } from '@angular/router';
import { SignUp } from './Feature/Auth/sign-up/sign-up';
import { SignIn } from './Feature/Auth/sign-in/sign-in';

export const routes: Routes = [
  { path: 'sign-up', component: SignUp },
  { path: 'sign-in', component: SignIn },

  // Lazy load UserDashboard module
  {
    path: 'user-dashboard',
    loadChildren: () =>
      import('./Feature/user-dashboard/user-dashboard.module').then(
        (m) => m.UserDashboardModule
      ),
  },

  // Default redirect
  { path: '', redirectTo: 'sign-up', pathMatch: 'full' },
  { path: '**', redirectTo: 'sign-up' }, // wildcard for unknown routes
];
