import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { IWeightHistory } from '../type/weight-history.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-weight-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CommonModule
  ],
  template: `
    <h1 mat-dialog-title>{{ isEditMode ? 'Edit Weight' : 'Add New Weight' }}</h1>

    <form [formGroup]="weightForm">
      <div mat-dialog-content>
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Date</mat-label>
          <input matInput type="date" formControlName="date">
          <mat-error *ngIf="weightForm.get('date')?.hasError('required') && weightForm.get('date')?.touched">
            Date is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Weight (kg)</mat-label>
          <input matInput type="number" formControlName="weightKg">
          <mat-error *ngIf="weightForm.get('weightKg')?.hasError('required') && weightForm.get('weightKg')?.touched">
            Weight is required
          </mat-error>
          <mat-error *ngIf="weightForm.get('weightKg')?.hasError('min') && weightForm.get('weightKg')?.touched">
            Weight must be greater than 0
          </mat-error>
        </mat-form-field>
      </div>

      <div mat-dialog-actions align="end">
        <button mat-button (click)="onCancel()">Cancel</button>
        <button mat-raised-button color="primary" (click)="onSave()" [disabled]="weightForm.invalid">
          Save
        </button>
      </div>
    </form>
  `,
  styleUrls: ['./add-weight-dialog.scss'],
})
export class AddWeightDialog {
  fb = inject(FormBuilder);
  dialogRef = inject(MatDialogRef<AddWeightDialog>);
  data = inject(MAT_DIALOG_DATA) as Partial<IWeightHistory> | null;

  weightForm: FormGroup;
  isEditMode = false;

  constructor() {
    this.isEditMode = !!(this.data && this.data.whId && this.data.whId !== 0);

    this.weightForm = this.fb.group({
      date: [this.formatDateInput(this.data?.date), Validators.required],
      weightKg: [
        this.data?.weightKg ?? '',
        [Validators.required, Validators.min(0.1)]
      ],
    });
  }

  private formatDateInput(date?: Date | string): string {
    const d = date ? new Date(date) : new Date();
    const tzOff = d.getTimezoneOffset() * 60000;
    return new Date(d.getTime() - tzOff).toISOString().slice(0, 10);
  }

  onCancel() {
    this.dialogRef.close();
  }

  onSave() {
    if (this.weightForm.valid) {
      const formValue = this.weightForm.value;
      const result: IWeightHistory = {
        whId: this.data?.whId ?? 0,
        petId: this.data?.petId ?? 0,
        date: new Date(formValue.date),
        weightKg: formValue.weightKg
      };
      this.dialogRef.close(result);
    }
  }
}
