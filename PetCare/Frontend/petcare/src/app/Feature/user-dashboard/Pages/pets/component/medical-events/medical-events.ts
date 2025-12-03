import { Component, inject } from '@angular/core';
import { MedicalEventService } from './service/medical-event-service';
import { CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { IMedicalEvent, IMedicalEventUpdate } from './type/medical-events.model';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { AddMedicalEventDialog } from './add-medical-event-dialog/add-medical-event-dialog';
export enum MedicalType {
  Vaccination = 0,
  VetVisit = 1,
  Medication = 2,
  Surgery = 3,
  Other = 4
}

@Component({
  selector: 'app-medical-events',
  standalone: true,
  imports: [CommonModule, DatePipe, MatDialogModule, MatButtonModule],
  templateUrl: './medical-events.html',
  styleUrl: './medical-events.scss',
})
export class MedicalEvents {
httpService = inject(MedicalEventService);
medicalTypes=MedicalType;
router = inject(Router);
MedicalEventList: IMedicalEvent[] = [];
MedicalEventUpate:IMedicalEventUpdate[]=[];
id: number = 0;
route = inject(ActivatedRoute)
dialog = inject(MatDialog);
   ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['petId'];
      this.httpService.getmedicalEvents(this.id).subscribe(data => {
        this.MedicalEventList = data;
        console.log( this.MedicalEventList)
      });
    });
  }
  deleteMedicalEvent(id: number) {
    this.httpService.deleteMedicalEvent(id).subscribe(() => {
      this.MedicalEventList = this.MedicalEventList.filter(me => me.id !== id);
    });
  }
  AddAttachment(id:number){
   this.router.navigate(['/user-dashboard/attachment', id], { relativeTo: this.route });
   
  }
  openDialog(event?: IMedicalEvent) {
    const dialogRef = this.dialog.open(AddMedicalEventDialog, {
      width: '500px',
      data: event ?? null
    });

    dialogRef.afterClosed().subscribe((result: IMedicalEvent | null) => {
      if (!result) return;

      if (event) {
        const payload: Partial<IMedicalEventUpdate> = {
          petId: event.petId,
          date: result.date,
          type: result.type,
          veterinarian: result.vetName,
          notes: result.notes,
          nextFollowupDate: result.nextFollowupDate 
        };
        this.httpService.updateMedicalEvent(event.id, payload).subscribe({
          next: (updated: IMedicalEvent) => {
            const idx = this.MedicalEventList.findIndex(m => m.id === event.id);
            if (idx > -1) this.MedicalEventList[idx] = updated;
            this.ngOnInit();
          },
          error: (err) => {
            console.error('Update MedicalEvent error body:', err.error);
            console.log('Payload:', payload);
          }
        });
      } else {
        const toCreate: IMedicalEvent = {
          id: 0,
          petId: this.id,
          date: result.date,
          type: result.type,
          vetName: result.vetName,
          notes: result.notes,
          nextFollowupDate: result.nextFollowupDate ?? null
        };
        this.httpService.addMedicalEvent(toCreate).subscribe({
          next: (created: IMedicalEvent) => {
            this.MedicalEventList.push(created);
            this.ngOnInit();
          },
          error: (err) => {
            console.error('Add MedicalEvent error body:', err.error);
          }
        });
      }
    });
  }
  
}
