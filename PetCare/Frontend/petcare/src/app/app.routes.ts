import { Routes } from '@angular/router';
import { SignUp } from './Feature/Auth/sign-up/sign-up';
import { SignIn } from './Feature/Auth/sign-in/sign-in';

export const routes: Routes = [
  { path: 'sign-up', component: SignUp },
  { path: 'sign-in', component: SignIn },
  { path: '', redirectTo: 'sign-up', pathMatch: 'full' }
];