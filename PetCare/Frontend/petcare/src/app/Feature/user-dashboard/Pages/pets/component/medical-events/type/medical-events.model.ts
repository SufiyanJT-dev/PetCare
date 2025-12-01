export interface IMedicalEvent {
  id: number;
  petId: number,
  date: Date;
  type: number;
  vetName: string;
  notes: string;
  nextFollowupDate?: Date | null;
}
export interface IMedicalEventUpdate {
  id: number;
  petId: number,
  date: Date;
  type: number;
  veterinarian: string;
  notes: string;
  nextFollowupDate?: Date | null;
}
