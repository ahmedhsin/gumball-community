import { AuthService } from 'src/app/services/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent {
  constructor (private authService:AuthService){}
  username: string = '';
  Firstname : string='';
  Lastname :string ='';
  email : string ='';
  password: string = '';
    onSignup() {
      this.authService.siginUp({
        username: this.username,
        Firstname: this.Firstname,
        Lastname: this.Lastname,
        email: this.email,
        password: this.password
      }).subscribe(
        response => {
          console.log('Signup successful', response);
        },
        error => {
          console.error('Signup error', error);
        }
      );
    }

    ngOnInit(): void {
      if(this.authService.isAuthenticated){
        this.authService.logout();
      }
    }
}
