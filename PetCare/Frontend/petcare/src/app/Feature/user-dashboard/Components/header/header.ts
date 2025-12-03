import { Component, EventEmitter, Output } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    RouterModule
  ],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  constructor(private router:Router){}
  @Output() menuToggled = new EventEmitter<void>();

  toggleMenu(): void {
    console.log('Header clicked');   
    this.menuToggled.emit();
  }
  logout(){
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken'); 
   this.router.navigate(['/login'], { replaceUrl: true });
  }
  goToPRofile(){
    this.router.navigate(['user-dashboard/profile'])
  }
}