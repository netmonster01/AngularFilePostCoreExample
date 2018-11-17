import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service';
import { Observable } from 'rxjs/Observable';
import { RegisterUser } from '../models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  user: RegisterUser;

  constructor(private authService: AuthService) {
    this.user = new RegisterUser();
  }

  didCreate = false;

  imageSrc  = '/assets/uploadIcon.png';

;  ngOnInit() {
  }

  submitData() {
    this.authService.register(this.user).subscribe((didCreate: boolean) => this.processRegister(didCreate));;
  }

  processRegister(didCreate: boolean): void {

    this.didCreate = didCreate;
    console.log(didCreate);

  }

  onFileSelected(event) {
    // set image on user
    this.user.avatarImage = <File>event.target.files[0];
    console.log(this.user.avatarImage);
    // setup reader to read input.
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imageSrc = e.target.result;
    };

    reader.readAsDataURL(this.user.avatarImage);
  }

}
