import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Menu } from '../menu.model';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-menu-item',
  standalone: true,
  imports: [
    CommonModule,
    MatListModule,
    MatIconModule,
    MatExpansionModule,
    RouterModule
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './menu-item.html',
  styleUrl: './menu-item.scss',
})
export class MenuItem {
  @Input() menu: Menu = [];
   opened = false;

  toggle(): void {
    this.opened = !this.opened;
    console.log('Sidenav opened:', this.opened);
  }

}