import { HTTP_INTERCEPTORS, HttpEvent, HttpEventType, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';

import { inject, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { AuthService } from '../Services/api/Authorization/auth.service';
import { Observable, tap } from 'rxjs';

export function apiRequestInterceptor(
    req: HttpRequest<unknown>,
    next: HttpHandlerFn): Observable<HttpEvent<unknown>> {

  var userRequest = inject(AuthService);

  const tokenizeReq = req.clone({
    headers: req.headers.append('Authorization', `bearer ${userRequest.token}`)
  })
  return next(tokenizeReq);
};
