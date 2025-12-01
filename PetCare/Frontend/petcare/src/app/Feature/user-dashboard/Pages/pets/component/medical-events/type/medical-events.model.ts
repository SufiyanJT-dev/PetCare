export interface IMedicalEvent {
  id: number;
  petId: number,
  date: Date |null;
  type: number;
  vetName: string;
  notes: string;
  nextFollowupDate?: Date | null;
}
export interface IMedicalEventUpdate {
  id: number;
  petId: number,
  date: Date | null;
  type: number;
  veterinarian: string;
  notes: string;
  nextFollowupDate?: Date|null ;
}
