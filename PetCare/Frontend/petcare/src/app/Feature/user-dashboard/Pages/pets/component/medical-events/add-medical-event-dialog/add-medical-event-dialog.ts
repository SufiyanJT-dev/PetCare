import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { IMedicalEvent } from '../type/medical-events.model';
import { MatOption, MatSelectModule } from '@angular/material/select';
import { minDate } from './CustomValidator/minDate';

@Component({
  selector: 'app-add-medical-event-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './add-medical-event-dialog.html',
  styleUrls: ['./add-medical-event-dialog.scss']
})
export class AddMedicalEventDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<AddMedicalEventDialog>);
  private data = inject(MAT_DIALOG_DATA) as IMedicalEvent | null;

  isEditMode = false;
medicalTypes = [
  { value: 0, label: 'Vaccination' },
  { value: 1, label: 'Vet Visit' },
  { value: 2, label: 'Medication' },
  { value: 3, label: 'Surgery' },
  { value: 4, label: 'Other' }
];

  eventForm = this.fb.group({
    date: ['', [Validators.required]],
    type: ['', Validators.required], // numeric but stored as string until save
    vetName: ['', Validators.required],
    notes: [''],
    nextFollowupDate: ['',minDate(new Date())]
  });

  constructor() {
    if (this.data) {
      this.isEditMode = true;
      this.eventForm.patchValue({
        date: this.data.date?this.formatDate(this.data.date): '',
        type: String(this.data.type),
        vetName: this.data.vetName,
        notes: this.data.notes,
        nextFollowupDate: this.data.nextFollowupDate ? this.formatDate(this.data.nextFollowupDate) : ''
      });
    }
  }

  private formatDate(date: Date | string | null | undefined): string {
    if (!date) return '';
    return new Date(date).toISOString().slice(0, 10);
  }

  cancel() {
    this.dialogRef.close();
  }

  save() {
    if (this.eventForm.invalid) return;
    const v = this.eventForm.value;
    const result: IMedicalEvent = {
      id: this.data?.id ?? 0,
      petId: this.data?.petId ?? 0,
      date: v.date ? new Date(v.date): null,
      type: v.type ? Number(v.type) : 0,
      vetName: v.vetName ?? '',
      notes: v.notes ?? '',
      nextFollowupDate: v.nextFollowupDate ? new Date(v.nextFollowupDate) : null
    };
    
    this.dialogRef.close(result);
  }
}
