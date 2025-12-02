import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Remainder {
  constructor(private http:HttpClient){}
  private baseUrl:string="https://localhost:7121/api/Reminders/"
    getAllRemainder(petId:number):Observable<any>{
    return this.http.get(`${this.baseUrl}GetReminderByPetId/${petId}`)
  }
  getAllPreviousReminder(petId:number):Observable<any>{
    return this.http.get(`${this.baseUrl}GetPreviousReminders/${petId}`)
  }
 
}
