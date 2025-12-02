import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestForAddPet } from '../component/add-pet-dialog/Type/RequestForAddPet';

@Injectable({
  providedIn: 'root',
})
export class Api {
  constructor(private http:HttpClient){}
  private baseUrl:string="https://localhost:7121/api/pets/"
  getAllpetByUserID(ownerId:number):Observable<any>{
    return this.http.get(`${this.baseUrl}GetPetsByOwnerId${ownerId}`)
  }
  addPetByUserID(payload:RequestForAddPet):Observable<any>{
    return this.http.post(`${this.baseUrl}AddPet`,payload)
  }
  deletePet(petid:number):Observable<any>{
    return this.http.delete(`${this.baseUrl}DeletePet${petid}`)
  }
  updatePet(petId:number,payload:RequestForAddPet):Observable<any>{
     return this.http.patch(`${this.baseUrl}UpdatePetDetails${petId}`,payload)
  }
 
}
