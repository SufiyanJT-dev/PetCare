import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ISignUp } from './type/signup';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from "@angular/router";
import { Services } from './services/services';

@Component({
  selector: 'app-sign-up',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './sign-up.html',
  styleUrls: ['./sign-up.scss'],
})
export class SignUp {
  signUpForm!: FormGroup;
 errorMessage: string | null = null; 
  constructor(private fb: FormBuilder,private createUserApi:Services,private router:Router) {}

  ngOnInit() {
    
    this.signUpForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required,Validators.pattern('^[6-9]\\d{9}$')]], 
      password: ['', [Validators.required,Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirm = form.get('confirmPassword')?.value;
    return password === confirm ? null : { mismatch: true };
  }

  submit() {
    if (this.signUpForm.valid) {
      const payload:ISignUp={
        fullname: this.signUpForm.value.name,
        email: this.signUpForm.value.email,
        phoneNumber: this.signUpForm.value.phoneNumber,
        password: this.signUpForm.value.password
      }
      this.createUserApi.signUpApi(payload).subscribe({
        next:(value)=>{
            alert(value.message);
            this.router.navigate(["/sign-in"]);
        },
        error:(err)=>{
          this.errorMessage = err.error?.errors?.[0] || 'Something went wrong';
          alert(this.errorMessage+" use a different mail")
        }
      })
      console.log(payload)
    } else {
      console.log('Form Invalid');
      this.signUpForm.markAllAsTouched();
    }
  }

  isInvalid(field: string) {
    const control = this.signUpForm.get(field);
    return control && control.invalid && (control.touched || control.dirty);
  }
}
