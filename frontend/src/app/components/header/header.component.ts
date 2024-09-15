import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Input() author: any; // Accept author as an input
  isAuthenticated: boolean;

  constructor(private authService: AuthService) {
    this.isAuthenticated = this.authService.isAuthenticated;
  }

  ngOnInit(): void {
    console.log('HeaderComponent ngOnInit called'); // Debug log

    // Mock data for testing
    this.author = this.author || {
      name: 'John Doe',
      image: 'assets/images/profile.png'
    };

    // Uncomment to use real service data
    /*
    this.authService.getAuthor().subscribe(data => {
      this.author = data;
    });
    */
  }

  logout(): void {
    this.authService.logout();
  }
}
