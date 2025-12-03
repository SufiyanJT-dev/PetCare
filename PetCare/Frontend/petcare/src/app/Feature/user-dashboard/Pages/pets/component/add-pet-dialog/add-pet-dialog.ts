import { Component, Inject, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs/operators';

import { maxDateValidator } from '../customValidators/maxDateValidator';
import { RequestForAddPet } from './Type/RequestForAddPet';
import { Api } from '../../service/api';
import { GetIdServices } from '../../../../../../Shared/Service/get-id-services';

// Enum for backend numeric values
export enum Species {
  Dog = 0,
  Cat = 1,
  Bird = 2,
  Reptile = 3,
  Other = 4
}

interface PetDialogData {
  petId?: number;
  name?: string;
  species?: number;
  breed?: string;
  dateOfBirth?: string;
}

@Component({
  selector: 'app-add-pet-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './add-pet-dialog.html',
  styleUrls: ['./add-pet-dialog.scss'],
})
export class AddPetDialog implements OnInit {
  petForm: FormGroup;
  isEditMode = false;
  isSubmitting = false;

  // Species dropdown: numeric value and label
  speciesOptions = [
    { label: 'Dog', value: Species.Dog },
    { label: 'Cat', value: Species.Cat },
    { label: 'Bird', value: Species.Bird },
    { label: 'Reptile', value: Species.Reptile },
    { label: 'Other', value: Species.Other }
  ];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddPetDialog>,
    private apiForPet: Api,
    private getUserId: GetIdServices,
    @Inject(MAT_DIALOG_DATA) public data: PetDialogData | null
  ) {
    this.petForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.data) {
      this.isEditMode = true;
      this.populateForm(this.data);
    }
  }

  private createForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required]],
      species: [null, [Validators.required]],  // will store numeric value
      breed: ['', [Validators.required]],
      dob: ['', [Validators.required, maxDateValidator(new Date())]]
    });
  }

  private populateForm(data: PetDialogData): void {
    this.petForm.patchValue({
      name: data.name || '',
      species: data.species ?? null,
      breed: data.breed || '',
      dob: data.dateOfBirth ? this.formatDateForInput(data.dateOfBirth) : ''
    });
  }

  private formatDateForInput(dateString: string): string {
    return new Date(dateString).toISOString().substring(0, 10);
  }

  private buildPayload(): RequestForAddPet {
    const formValue = this.petForm.value;
    return {
      ownerId: this.getUserId.getUserID(),
      name: formValue.name,
      species: formValue.species,  // numeric value
      breed: formValue.breed,
      dateOfBirth: new Date(formValue.dob)
    };
  }

  onSave(): void {
    if (this.petForm.invalid) {
      this.petForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const payload = this.buildPayload();

    const request$ = this.isEditMode && this.data?.petId
      ? this.apiForPet.updatePet(this.data.petId, payload)
      : this.apiForPet.addPetByUserID(payload);

    request$
      .pipe(finalize(() => this.isSubmitting = false))
      .subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => {
          console.error('Error saving pet:', err);
        }
      });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  get formControls() {
    return this.petForm.controls;
  }
}
