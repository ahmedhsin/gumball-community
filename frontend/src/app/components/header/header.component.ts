import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  author: any;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    console.log('HeaderComponent ngOnInit called'); // Debug log

    // Use mock data for testing
    this.author = {
      name: 'John Doe',
      image: 'assets/images/profile.png'
    };

    // If using the real service later, uncomment the lines below
    /*
    this.authService.getAuthor().subscribe(data => {
      this.author = data;
    });
    */
  }
}
