import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from '../type/signin';

@Injectable({
  providedIn: 'root',
})
export class authServices {
  constructor(private http:HttpClient){}
   baseUrl:string='https://localhost:7121/api/auth/';
  login(login:LoginRequest):Observable<any>{
    return this.http.post(`${this.baseUrl}login`,login)
  }

  
}
