import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../service';
import { User } from '../models';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
    
  public forecasts: WeatherForecast[];

  users: User[] = [];

  constructor(private auth: AuthService) {
    this.getUsers();
  }

  getUsers(): void {
    this.auth.getUsers().subscribe((users: User[]) => this.processData(users));
  }


  processData(users: User[]): any {
    this.users = users;
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
