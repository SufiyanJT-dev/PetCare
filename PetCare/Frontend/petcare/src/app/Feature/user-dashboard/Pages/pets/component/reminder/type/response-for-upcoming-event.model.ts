export interface ReminderView {
  date: string;
  type: number;
  vetName: string;
  notes: string;
  nextFollowupDate: string;
  petName: string;
  breed: string;
}
export enum ReminderType {
  Vaccination = 0,
  VetVisit = 1,
  Medication = 2,
  Surgery = 3,
  Other = 4
}
