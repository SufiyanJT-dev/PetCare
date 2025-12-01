import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AttachmentService {
  private http = inject(HttpClient);
  
  baseUrl:string="https://localhost:7121/api/Attachment/";
  getAllByMedicalId(id:number):Observable<any>{
    return this.http.get<Response>(`${this.baseUrl}GetByEventId${id}`) 
  }
  addMedical(formData:FormData):Observable<any>{
     return this.http.post(`${this.baseUrl}`,formData)

  }
  updateMedical(id:number,formData:FormData){
    return this.http.patch(`${this.baseUrl}${id}`,formData)
  }
  deleteMedical(id:number){
    return this.http.delete(`${this.baseUrl}${id}`)
  }
}
