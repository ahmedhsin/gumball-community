import { Component, Input } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.css']
})
export class ProfileHeaderComponent {
  @Input() author: any;
}
