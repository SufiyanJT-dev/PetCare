import { Component, signal } from '@angular/core';

import { AddPetDialog } from './component/add-pet-dialog/add-pet-dialog';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { Api } from './service/api';
import { ResponseForGetById } from './Type/ResponseForGetById';
import { GetIdServices } from '../../../../Shared/Service/get-id-services';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-pets',
  imports: [
    CommonModule,
    MatDialogModule,        
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatListModule,
    MatExpansionModule,
    MatToolbarModule,
    RouterOutlet
  ],
  templateUrl: './pets.html',
  styleUrl: './pets.scss',
})
export class Pets {

  userId:number=0;
  petsDetails = signal<ResponseForGetById[]>([]);
  constructor(private dialog: MatDialog,private route:ActivatedRoute,private apiPets:Api,private getIdServices :GetIdServices,private router:Router) {}
  ngOnInit(){
   
    this.userId = this.getIdServices.getUserID();
    this.apiPets.getAllpetByUserID(this.userId).subscribe({
      next:(value)=>{
       this.petsDetails.set(value);
        console.log(this.petsDetails)

      },
      error:(err)=>{
          console.log(err)
      }
    })
  }
 onEditPet(pet: ResponseForGetById) {
  console.log('Edit pet:', pet);
  
  this.openDialog(pet);  
}
gotoViewAllReminder(petId:number){

}
gotoViewAllDocuments(petId:number){

}
gotoViewAllMedicalEvents(petId:number){

}
healthStatic(petId:number){
this.router.navigate(['/user-dashboard/weight-history', petId], { relativeTo: this.route });
}
onDeletePet(pet: ResponseForGetById) {
  if (confirm(`Are you sure you want to delete ${pet.name}?`)) {
    this.apiPets.deletePet(pet.petId).subscribe({
      next: () => {
        console.log(`${pet.name} deleted`);
      
        this.ngOnInit()
      },
      error: err => console.log(err)
    });
  }
}

openDialog(pet?: ResponseForGetById) {
  const dialogRef = this.dialog.open(AddPetDialog, {
    width: '400px',
    data: pet 
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      this.ngOnInit(); 
    }
  });
}

}
