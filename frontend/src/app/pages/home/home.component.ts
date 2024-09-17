import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private postService: PostService, private authService: AuthService) { }
  isAuthenticated = this.authService.isAuthenticated;
  author = this.authService.getAuthor();
  posts: any = []
  
  handleNewPost(postData: any) {
    this.posts.unshift(postData);
  }
  ngOnInit(): void {
    this.postService.getPosts().subscribe((res: any) => {
      this.posts = [...res]
      console.log(res);
    })
    
  }
}
