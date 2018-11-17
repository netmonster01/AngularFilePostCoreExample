import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterUser, User } from '../models';
import { Observable } from 'rxjs/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {}

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
