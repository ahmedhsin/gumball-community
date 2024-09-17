import { Component, Input } from '@angular/core';
import { environment } from 'environment';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent {
  @Input() post:any;
  @Input() addReply:any;
  @Input() parentComment:any;
  url = environment.url;
  author = this.authService.getAuthor();
  constructor(private postService: PostService,private authService: AuthService){}
}
