import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-in',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.scss',
})
export class SignIn implements OnInit{
loginForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  // Helper to check if field is invalid
  isInvalid(field: string) {
    const control = this.loginForm.get(field);
    return control && control.invalid && (control.touched || control.dirty);
  }

  submit() {
    if (this.loginForm.valid) {
      console.log('Login data:', this.loginForm.value);
      // Call your login service here
    } else {
      console.log('Form Invalid');
      this.loginForm.markAllAsTouched(); // show errors for all fields
    }
  }
}