import { Component } from '@angular/core';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent {
  author:any = {
    name: 'Ahmed Mubarak',
    image: 'assets/images/profile.png'
  }
}
