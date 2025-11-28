import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './Core/Layout/navbar/navbar';
import { Footer } from './Core/Layout/footer/footer';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,Footer ],
  templateUrl: './app.html',
  
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('petcare');
}
