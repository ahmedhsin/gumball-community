import { AuthService } from 'src/app/services/auth.service';
import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent {
  constructor (private route:Router,private authService:AuthService){}
  Firstname : string='';
  Lastname :string ='';
  Email : string ='';
  password: string = '';
  selectedFile: any;
  imageFile:any;
  SignUp() {
      const formData = new FormData();
      formData.append('firstname', this.Firstname);
      formData.append('lastname', this.Lastname);
      formData.append('email', this.Email);
      formData.append('password', this.password);
      if (this.imageFile)
        formData.append('imageFile', this.imageFile, this.imageFile.name);
      this.authService.siginUp(formData).subscribe(
        response => {
          this.route.navigate(['/login'])
        },
        error => {
          alert('Error Occurd')
        }
      );
    }

    ngOnInit(): void {
      if(this.authService.isAuthenticated){
        this.authService.logout();
      }
    }

    onFileSelected(event: any): void {
      const file = event.target.files[0];
      this.imageFile = file;
    }
}
