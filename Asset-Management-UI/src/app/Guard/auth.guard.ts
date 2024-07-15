import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  let isloggedIn=sessionStorage.getItem("isLoggedIn");
  if (isloggedIn=='false'){
    alert("Please Login. Redirecting to login page");
    router.navigate(['login']);
  }
  return true;
};
