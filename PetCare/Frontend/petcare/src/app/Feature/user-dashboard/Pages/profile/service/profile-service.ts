import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IUserProfile } from '../type/profile.model';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  http = inject(HttpClient)

  getUserProfile(id : number) {
    return this.http.get<IUserProfile>('https://localhost:7121/api/User/GetUserById/'+id);
  }
}
