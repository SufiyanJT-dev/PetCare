import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Header } from '../header/header';
import { Menu } from './menu.model'
import { MenuItem } from './menu-item/menu-item';
import { RouterOutlet } from '@angular/router';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [MatSidenavModule,MatIconModule,MatExpansionModule,Header,MenuItem,RouterOutlet],
  templateUrl: './layout.html',
  styleUrls: ['./layout.scss'],
})
export class Layout {
  
   menu: Menu = [
    {
      title: 'Pet Management',
      icon: 'pets',
      link: '/user-dashboard/pet-management',
      color: 'rgba(47, 5, 255, 1)',
    },
   {
    title: 'Upcoming Reminders',
      icon: 'notifications',
      link: '/user-dashboard/upcoming-reminders',
      color: 'rgba(47, 5, 255, 1)',
   }
  ];

  opened = false;

  toggle(): void {
    this.opened = !this.opened;
    console.log('Layout toggle called, opened =', this.opened);
  }
}
