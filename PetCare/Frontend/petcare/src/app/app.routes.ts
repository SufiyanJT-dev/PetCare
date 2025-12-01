import { Routes } from '@angular/router';
import { SignUp } from './Feature/Auth/sign-up/sign-up';
import { SignIn } from './Feature/Auth/sign-in/sign-in';
import { UserDashboard } from './Feature/user-dashboard/user-dashboard';
import { Profile } from './Feature/user-dashboard/Pages/profile/profile';
import { Pets } from './Feature/user-dashboard/Pages/pets/pets';
import { Weighthistory } from './Feature/user-dashboard/Pages/pets/component/weighthistory/weighthistory';
import { MedicalEvents } from './Feature/user-dashboard/Pages/pets/component/medical-events/medical-events';
import { Remainder } from './Feature/user-dashboard/Pages/pets/component/medical-events/component/remainder/remainder';
import { AddAttachment } from './Feature/user-dashboard/Pages/pets/component/medical-events/component/add-attachment/add-attachment';

export const routes: Routes = [
  { path: 'sign-up', component: SignUp },
  { path: 'sign-in', component: SignIn },
  
  {path: 'user-dashboard',component:UserDashboard,
    children:[
      {path:'profile',component:Profile},
      {path:'Pet-management',component:Pets},
     {path:'weight-history/:petId',component:Weighthistory},
     {path:'medical-events/:petId',component:MedicalEvents},
     {path: 'reminders/:petId', component: Remainder},
     {path:'attachment/:Id',component:AddAttachment}
    ]},
  


  
  { path: '', redirectTo: 'sign-up', pathMatch: 'full' },
  { path: '**', redirectTo: 'sign-up' },
];
