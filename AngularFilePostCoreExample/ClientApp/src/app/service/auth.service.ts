import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterUser, User } from '../models';
import { Observable } from 'rxjs/Observable';

import { BehaviorSubject } from 'rxjs';

export const ANONYMOUS_USER: User = {
  password: null,
  email: null,
  id: null,
  roles: [],
  avatarImage: null,
  firstName: null,
  lastName: null,
  isAdmim: false,
  token: null,
  userName: null
};

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private subject = new BehaviorSubject<User>(ANONYMOUS_USER);

  user$: Observable<User> = this.subject.asObservable();

  isLoggedIn$: Observable<boolean> = this.user$.map(user => !!user.id);

  isLoggedOut$: Observable<boolean> = this.isLoggedIn$.map(isLoggedIn => !isLoggedIn);
  storagekey = 'loggedInUser';

  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

  options = {
    headers: this.headers
  };

  constructor(private http: HttpClient) {}

  
  login(email: string, password: string) {
    // set options

    return this.http.post<User>('/api/Account/Login', { email, password }, this.options).shareReplay().do(user => console.log(user));
  }

  register(newUser: RegisterUser) {

    var data = new FormData();
    data.append("avatarImage", newUser.avatarImage);
    data.append("email", newUser.email);
    data.append("firstName", newUser.firstName);
    data.append("lastName", newUser.lastName);
    data.append("password", newUser.password);
    data.append("userName", newUser.userName);

    return this.http.post<boolean>('/api/Account/Register', data).catch(this.handleError);// .subscribe((data: any) => console.log(data));
  }

  handleError(handleError: any): any {
    console.log(handleError);
  }

  getUsers() {
    return this.http.get<User[]>('/api/Account/Users').catch(this.handleError);
  }
}
