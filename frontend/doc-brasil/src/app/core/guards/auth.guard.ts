import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);
  const token = authService.getToken();

  if(token && !authService.isTokenExpired(token)){
    if(state.url === '/' || state.url === '' && !authService.isTokenExpired(token)){
      router.navigate(['/painel/associados']);
      return false;
    }

    return true;
  } 
  
  if (state.url.startsWith('/painel')) {
      router.navigate(['/']);
    return false;
  }
  
  return true;
};
