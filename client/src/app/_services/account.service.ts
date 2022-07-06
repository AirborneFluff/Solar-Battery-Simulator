import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User, UserLogin } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  login(model: UserLogin) {
    return this.http.post(this.baseUrl + "account/login", model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.router.navigate([""]);
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
