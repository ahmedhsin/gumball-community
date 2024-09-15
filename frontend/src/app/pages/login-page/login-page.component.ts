import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  constructor(private authService: AuthService, private router:Router) {}
  email: string = '';
  password: string = '';
   onLogin() {
    this.authService.login(this.email, this.password).subscribe(
      response => {
        this.router.navigate(['/']);
      },
      error => {
        alert('wrong Credintals')
        console.error('Login error', error);
      }
    );
  }

  ngOnInit(): void {
    if(this.authService.isAuthenticated){
      this.authService.logout();
    }
  }
}
