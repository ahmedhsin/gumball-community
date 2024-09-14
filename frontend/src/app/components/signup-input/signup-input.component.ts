import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-signup-input',
  templateUrl: './signup-input.component.html',
  styleUrls: ['./signup-input.component.css']
})
export class SignupInputComponent {
  username: string = '';
  Firstname : string='';
  Lastname :string ='';
  email : string ='';
  password: string = '';

  @Output() signupinput = new EventEmitter<{username:string ,Firstname :string,Lastname :string,email:string, password:string}>();

  onSignup() {
    this.signupinput.emit({username: this.username,Firstname:this.Firstname,Lastname:this.Lastname,email:this.email, password: this.password});
  }
}
