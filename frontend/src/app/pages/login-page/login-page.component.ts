import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  constructor(private authService: AuthService) {}

   onLogin(data: {username: string, password: string}) {
    this.authService.login(data.username, data.password).subscribe(
      response => {
        // Handle login success
        console.log('Login successful', response);
      },
      error => {
        // Handle login error
        console.error('Login error', error);
      }
    );
  }
}
