import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GetIdServices {
  getUserID(){
    if (typeof window !== 'undefined') {
  

     const token=localStorage.getItem('accessToken')||''
      const decodedToken = decodeToken(token);
    return decodedToken.sub;
    }
   
  }
}
function decodeToken(token: string) {
  const payload = token.split('.')[1]; 
  const decoded = atob(payload);       
  return JSON.parse(decoded);          
}
