import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Response} from '../type/Response.model'
import { ISignUp } from '../type/signup';

@Injectable({
  providedIn: 'root',
})
export class Services {
  constructor(private https:HttpClient){}
  baseUrl:string='https://localhost:7121/api/User/'
 signUpApi(payload: ISignUp): Observable<any> {
  return this.https.post(`${this.baseUrl}CreateUser`,payload, { withCredentials: true });
  
  }
}
