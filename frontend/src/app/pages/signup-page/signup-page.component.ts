import { AuthService } from 'src/app/services/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent {
  constructor (private authservice:AuthService){}

    onSignup(data: {username:string ,Firstname :string,Lastname :string,email:string, password:string}) {
      this.authservice.signup(data.username,data.Firstname,data.Lastname,data.email, data.password).subscribe(
        response => {
          // Handle login success
          console.log('Signup successful', response);
        },
        error => {
          // Handle login error
          console.error('Signup error', error);
        }
      );
    }
}
