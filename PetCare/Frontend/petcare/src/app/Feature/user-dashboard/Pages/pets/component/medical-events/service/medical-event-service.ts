import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IMedicalEvent, IMedicalEventUpdate } from '../type/medical-events.model';

@Injectable({
  providedIn: 'root',
})
export class MedicalEventService {
  http = inject(HttpClient);
  apiUrl = 'https://localhost:7121/api/MedicalEvent';

  getmedicalEvents(petId: number) {
    return this.http.get<IMedicalEvent[]>(`${this.apiUrl}/getbypetId/${petId}`);
  }
  deleteMedicalEvent(id: number) {
    return this.http.delete(`${this.apiUrl}/delete/${id}`);
}

 addMedicalEvent(medicalEvent: IMedicalEvent) {
  const payload = {
    PetId: medicalEvent.petId,
    Date: medicalEvent.date,
    Type: medicalEvent.type,
    VetName: medicalEvent.vetName,
    Notes: medicalEvent.notes,
    NextFollowupDate: medicalEvent.nextFollowupDate
      ? new Date(medicalEvent.nextFollowupDate).toISOString()
      : null
  };
  console.log("update",payload)
  return this.http.post<IMedicalEvent>(`${this.apiUrl}/AddMedicalEvent`, payload);
}

updateMedicalEvent(id: number, medicalEvent: Partial<IMedicalEventUpdate>) {
  const payload = {
    
    PetId: medicalEvent.petId ?? null,
    eventDate: medicalEvent.date ,
    Type: medicalEvent.type ?? null,
    veterinarian: medicalEvent.veterinarian ?? null,
    Notes: medicalEvent.notes ?? null,
    NextFollowupDate: medicalEvent.nextFollowupDate
      
     
  };
  console.log("update",payload)
  return this.http.patch<IMedicalEvent>(`${this.apiUrl}/update/${id}`, payload);
}
}