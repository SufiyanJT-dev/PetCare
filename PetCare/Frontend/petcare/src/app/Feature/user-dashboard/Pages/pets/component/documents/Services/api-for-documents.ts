import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AttachmentResponse } from '../Type/Response';

@Injectable({
  providedIn: 'root',
})
export class ApiForDocuments {
  constructor(private http:HttpClient){}
  private BaseUrl:string="https://localhost:7121/api";
  getAllByByPetId(id:number):Observable<AttachmentResponse[]>{
    return this.http.get<AttachmentResponse[]>(`${this.BaseUrl}/Attachment/GetByPetId${id}`)
  }
}
