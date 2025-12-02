import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileService } from './service/profile-service';
import { GetIdServices } from '../../../../Shared/Service/get-id-services';
import { IUserProfile } from './type/profile.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile {
userData: IUserProfile | null = null;
  httpService = inject(ProfileService)
  GetIdServices = inject(GetIdServices)
    getUserProfile(id:number){
    this.httpService.getUserProfile(id).subscribe(res=>{
     
      this.userData=res;
      console.log(this.userData);
    })}
  ngOnInit(){
    const id=this.GetIdServices.getUserID()
    this.getUserProfile(id)
  }
}
