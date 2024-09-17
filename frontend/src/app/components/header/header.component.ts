import { Component, Input } from '@angular/core';
import { environment } from 'environment';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  @Input() author: any;
  constructor(private authService: AuthService){}
  isAuthenticated = this.authService.isAuthenticated;
  url = environment.url
  logout(){
    this.authService.logout();
  }

}
