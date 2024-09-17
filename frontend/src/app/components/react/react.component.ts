import { Component, Input } from '@angular/core';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-react',
  templateUrl: './react.component.html',
  styleUrls: ['./react.component.css']
})
export class ReactComponent {
  @Input() setReact:any;
  @Input() post:any;

  constructor(private postService: PostService){}
  
}
