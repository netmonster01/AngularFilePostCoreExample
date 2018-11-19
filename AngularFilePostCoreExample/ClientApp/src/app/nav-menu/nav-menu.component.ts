import { Component } from '@angular/core';
import { AuthService, ANONYMOUS_USER } from '../service';
import { Observable } from 'rxjs/Observable';
import { User } from '../models';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isAuthenticated = false;

  user: User = ANONYMOUS_USER;

  constructor(private auth: AuthService) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
