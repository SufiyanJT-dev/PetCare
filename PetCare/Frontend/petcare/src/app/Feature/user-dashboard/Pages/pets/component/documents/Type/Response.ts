export interface AttachmentResponse {
  attachId: number;
  fileUrl: string;
  fileName: string;
  description: string;
  medicalEventId: number;

  medicalEvent: {
    id: number;
    date: string;
    
    notes?: string;
    nextFollowupDate?: string;
    pet: {
      petId: number;
      name: string;
      breed?: string;
    };
     type:number;
  };
}
export enum MedicalEventType {
  Vaccination = 0,
  VetVisit = 1,
  Medication = 2,
  Surgery = 3,
  Other = 4
}
