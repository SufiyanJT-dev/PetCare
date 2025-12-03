import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiForDocuments } from './services/api-for-documents';
import { AttachmentResponse, MedicalEventType } from './type/response.model';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-documents',
  imports: [DatePipe,CommonModule],
  templateUrl: './documents.html',
  styleUrl: './documents.scss',
})
export class Documents {
  constructor(private route:ActivatedRoute,private api:ApiForDocuments){}
  petId:number=0;
  attachments:AttachmentResponse[]=[];
  medicalEventType = MedicalEventType;
  ngOnInit(){
    this.route.params.subscribe(params=>{
      this.petId=+params['petId']
      console.log(this.petId);
      this.api.getAllByByPetId(this.petId).subscribe({
        next:(value)=>{
            
            this.attachments=value
            console.log(this.attachments)
        },
        error:(err)=>{
            console.log(err)
        }
      })
    })
  }
}
