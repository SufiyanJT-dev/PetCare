import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { Header } from './Components/header/header';
import { Layout } from './Components/layout/layout';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    RouterModule,
    Header,
    Layout
  ],
  templateUrl: './user-dashboard.html',
  styleUrl: './user-dashboard.scss',
})
export class UserDashboard {

}