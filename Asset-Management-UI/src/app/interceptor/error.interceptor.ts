import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  return next(req).pipe(
    catchError((error)=>{
      if([401, 403].includes(error.status)){
        alert("Unauthorized. Kindly login again.");
        localStorage.clear();
      sessionStorage.clear();
        router.navigate(['/login']);
      }
      const e = error.error.message || error.statusText;
      console.log(e);
      return throwError(()=>error);
    })
  );
};
                       