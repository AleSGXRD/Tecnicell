import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/api/Authorization/auth.service';

export const AuthGuard: CanActivateFn = (route, state) => {
  if(inject(AuthService).logged){
    return true;
  }
  else{
    inject(Router).navigate(['login'])
    return false;
  }
};
