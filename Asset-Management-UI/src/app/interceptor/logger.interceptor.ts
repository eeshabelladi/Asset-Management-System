import { HttpInterceptorFn } from '@angular/common/http';
import { StorageService } from '../storage/storage.service';
import { inject } from '@angular/core';
import { UserService } from '../user.service';

export const loggerInterceptor: HttpInterceptorFn = (req, next) => {
  
  console.log("Request is on its way");

  const userService = inject(UserService);
  const auth = userService.getCredentials();
    
  let authReq = req;
  if (auth) {
    authReq = req.clone({
      headers: req.headers.set('Authorization', 'Basic ' + auth),
    });
  }
  return next(authReq);
};
