import { Component, Input } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  @Input() author: any;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.getAuthor().subscribe(data => {
      this.author = data;
    });
  }
}
