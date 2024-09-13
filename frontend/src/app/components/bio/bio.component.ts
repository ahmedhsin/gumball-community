import { Component } from '@angular/core';

@Component({
  selector: 'app-bio',
  templateUrl: './bio.component.html',
  styleUrls: ['./bio.component.css']
})
export class BioComponent {
  LiveIn = 'cairo';
  BirthDate = '2-11-2002';
  Followers = 100;
  Following = 100;
}
