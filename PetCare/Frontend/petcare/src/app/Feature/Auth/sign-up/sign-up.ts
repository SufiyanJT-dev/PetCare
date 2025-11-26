import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ISignUp } from './type/signup';
import { CommonModule } from '@angular/common';
import { RouterLink } from "@angular/router";
@Component({
  selector: 'app-sign-up',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './sign-up.html',
  styleUrl: './sign-up.scss',
})
export class SignUp {
  signUpForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.signUpForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNo: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirm = form.get('confirmPassword')?.value;
    return password === confirm ? null : { mismatch: true };
  }

  submit() {
    // remove confirmPassword before submission
    if (this.signUpForm.valid) {
      console.log('Form Value:', this.signUpForm.value);
      // Call your registration service here
    } else {
      console.log('Form Invalid');
       this.signUpForm.markAllAsTouched(); // show errors for all fields
    }
  }
   isInvalid(field: string) {
    const control = this.signUpForm.get(field);
    return control && control.invalid && (control.touched || control.dirty);
  }
}