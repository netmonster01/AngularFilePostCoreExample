import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service';
import { Observable } from 'rxjs/Observable';
import { RegisterUser } from '../../models';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: RegisterUser;
  didCreate = false;
  imageSrc = '/assets/uploadIcon.png';

  constructor(private authService: AuthService) { }

  ngOnInit() {
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
