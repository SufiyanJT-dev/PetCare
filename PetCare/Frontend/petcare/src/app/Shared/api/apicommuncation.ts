import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class Apicommuncation {
  constructor(private http: HttpClient) {}

  getRefershToken(): Observable<{ accessToken: string }> {
    
    return this.http.post<{ accessToken: string }>(
      'https://localhost:7121/api/auth/refresh',{},
      {withCredentials:true}
    );
  }
}
