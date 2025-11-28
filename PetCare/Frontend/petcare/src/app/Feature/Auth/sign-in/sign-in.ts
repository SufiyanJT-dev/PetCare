import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequest } from './type/signin';
import { authServices } from './service/authServices';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.scss',
})
export class SignIn implements OnInit{
loginForm!: FormGroup;

  constructor(private fb: FormBuilder,private authLoginApi:authServices,private router: Router ) {}

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

 
  isInvalid(field: string) {
    const control = this.loginForm.get(field);
    return control && control.invalid && (control.touched || control.dirty);
  }

  submit() {
    const logindatapayload:LoginRequest={
      email:this.loginForm.value.email,
      password:this.loginForm.value.password

    }
    if (this.loginForm.valid) {
      this.authLoginApi.login(logindatapayload).subscribe({
        next:(value)=>{
            console.log(value)
            localStorage.setItem('accessToken',value.accessToken)
            this.router.navigate(['user-dashboard']);
        },
        error:(err)=>{
          console.log(err)
          alert(err.error.error)
        }
      })
      
    } else {
      console.log('Form Invalid');
      this.loginForm.markAllAsTouched(); 
    }
  }
}