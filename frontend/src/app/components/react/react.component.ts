import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-react',
  templateUrl: './react.component.html',
  styleUrls: ['./react.component.css']
})
export class ReactComponent {
  @Input() setReact:any;
  @Input() post:any;
  
}
