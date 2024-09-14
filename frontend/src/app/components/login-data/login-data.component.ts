import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-login-data',
  templateUrl: './login-data.component.html',
  styleUrls: ['./login-data.component.css']
})
export class LoginDataComponent {
  username: string = '';
  password: string = '';

  @Output() loginData = new EventEmitter<{username: string, password: string}>();

  onLogin() {
    this.loginData.emit({username: this.username, password: this.password});
  }
}
