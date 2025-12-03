import { Component } from '@angular/core';
import { Remainder } from './services/remainder';
import { ActivatedRoute } from '@angular/router';
import { ReminderType, ReminderView } from './type/response-for-upcoming-event.model';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-reminder',
  imports: [DatePipe,CommonModule,FormsModule],
  templateUrl: './reminder.html',
  styleUrl: './reminder.scss',
})
export class Reminder {
  petId:number=0;
  reminders:ReminderView[]=[]
  ReminderType = ReminderType;
  selectedType: number | null = null;
  reminderTypes = [
    {value:7,label:'previous Events'},
    { value: ReminderType.Vaccination, label: 'Vaccination' },
    { value: ReminderType.VetVisit, label: 'Vet Visit' },
    { value: ReminderType.Medication, label: 'Medication' },
    { value: ReminderType.Surgery, label: 'Surgery' },
    { value: ReminderType.Other, label: 'Other' }
  ];
constructor(private apiRemainders:Remainder,private route:ActivatedRoute){}
ngOnInit(){
this.route.params.subscribe(params=>{
this.petId=+params['petId']
})
 this.apiRemainders.getAllRemainder(this.petId).subscribe({
  next: (value: any[]) => {
    const mapped: ReminderView[] = value.map(rem => ({
      date: rem.date,
      type:rem.type,   
      vetName: rem.vetName,
      notes: rem.notes,
      nextFollowupDate: rem.nextFollowupDate,
      petName: rem.pet?.name,
      breed: rem.pet?.breed
    }));

    console.log(mapped);
   
    this.reminders = mapped;
  },
  error: (err) => {
    console.error(err);
  }
});
}
sortByType() {
    if (this.selectedType !== null && this.selectedType!==7) {
      this.reminders = [...this.reminders].sort((a, b) => {
        if (a.type === this.selectedType && b.type !== this.selectedType) return -1;
        if (a.type !== this.selectedType && b.type === this.selectedType) return 1;
        return 0;
      });
    }
    else if(this.selectedType !== null && this.selectedType===7){
      this.apiRemainders.getAllPreviousReminder(this.petId).subscribe({
      next: (value: any[]) => {

        
        const previousMapped: ReminderView[] = value.map(rem => ({
          date: rem.date,
          type: rem.type,
          vetName: rem.vetName,
          notes: rem.notes,
          nextFollowupDate: rem.nextFollowupDate,
          petName: rem.pet?.name,
          breed: rem.pet?.breed
        }));

        this.reminders = previousMapped;   // <<< THIS WAS MISSING
      },
      error: (err) => {
        console.error(err);
      }
    });
    }
    else{
      this.ngOnInit();
    }
  }
  getReminderTypeLabel(type: number): string {
  return ReminderType[type];
}

}
