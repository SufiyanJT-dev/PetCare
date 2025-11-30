import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { maxDateValidator } from '../customValidators/maxDateValidator';
import { RequestForAddPet } from './Type/RequestForAddPet';
import { Api } from '../../service/api';
import { GetIdServices } from '../../../../../../Shared/Service/get-id-services';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-pet-dialog',
   imports: [ MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ReactiveFormsModule,
    CommonModule], 
  templateUrl: './add-pet-dialog.html',
  styleUrl: './add-pet-dialog.scss',
})
export class AddPetDialog {
  petForm: FormGroup;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddPetDialog>,
    private apiForPet: Api,
    private router: Router,
    private getUserId: GetIdServices,
    @Inject(MAT_DIALOG_DATA) public data: any // <-- pet data injected
  ) {
    this.petForm = this.fb.group({
      name: ['', Validators.required],
      species: ['', Validators.required],
      dob: ['', [Validators.required, maxDateValidator(new Date())]]
    });

    if (data) {
      this.isEditMode = true;
      this.petForm.patchValue({
        name: data.name,
        species: data.breed, 
        dob: data.dateOfBirth ? new Date(data.dateOfBirth).toISOString().substring(0,10) : ''

      });
    }
  }

  onSave() {
    if (this.petForm.valid) {
      const payload: RequestForAddPet = {
        ownerId: this.getUserId.getUserID(),
        name: this.petForm.value.name,
        species: 0,
        breed: this.petForm.value.species,
        dateOfBirth: new Date(this.petForm.value.dob)
      };
 console.log(payload)
      if (this.isEditMode) {
       
        this.apiForPet.updatePet(this.data.petId, payload).subscribe({
          next: () => {
            this.dialogRef.close(true);
          },
          error: err => console.log(err)
        });
      } else {
        
        this.apiForPet.addPetByUserID(payload).subscribe({
          next: () => {
            console.log(payload)
            this.dialogRef.close(true);
          },
          error: err => console.log(err)
        });
      }
    } else {
      this.petForm.markAllAsTouched();
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
