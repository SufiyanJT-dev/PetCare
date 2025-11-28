import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Header } from '../header/header';
import { Menu } from './menu.model'
import { MenuItem } from './menu-item/menu-item';
import { RouterOutlet } from '@angular/router';
@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [MatSidenavModule,Header,MenuItem,RouterOutlet],
  templateUrl: './layout.html',
  styleUrls: ['./layout.scss'],
})
export class Layout {
  
  menu: Menu = [
  {
    title: 'Pet Management',
    icon: 'pets',
    link: '/user-dashboard/Pet-management', 
    color: '#2f05ffff',
  },
];

  opened = false;

  toggle(): void {
    this.opened = !this.opened;
    console.log('Layout toggle called, opened =', this.opened);
  }
}
