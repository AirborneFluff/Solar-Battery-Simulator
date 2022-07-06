import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { take, catchError } from 'rxjs/operators';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  unauthRedirectParams: Params;

  constructor(private accountService: AccountService, private router: Router, private route: ActivatedRoute) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);

    if (currentUser) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      });
    }
    return next.handle(request).pipe(
      catchError((requestError: HttpErrorResponse) => {
        if (requestError?.status === 401) {
          console.log("Unauth")
          this.router.navigate(['login'],{queryParams:{'redirectURL':this.router.url}});
        }
        if (requestError?.error == "No changes to apply") {
          return throwError(() => new Error("NoChanges"))
        }
        return throwError(() => new Error(requestError.message));
      })
    );
  }
}
