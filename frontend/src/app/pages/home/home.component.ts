import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  posts: Post[] = []

  handleNewPost(postData: any) {
    const post = {
      name:"Ahmed Mubarak",
      react:null,
      reactions:{
        1:0,
        2:0,
        3:0,
        4:0,
        5:0
      },
      date: "two hours ago",
      content:postData.content,
      image:postData.image,
      profile:"assets/images/profile.png",
      comments: []
    }

    this.posts.unshift(post);
  }
  constructor(private postService: PostService) { }

  ngOnInit(): void {
    this.postService.getPosts().subscribe((res: any) => {
      this.posts = res.map((post: any) => ({
        ...post,
        author: {
          name: post.authorName,  // Assuming the backend returns author name
          profile: post.authorProfile,  // Assuming the backend returns author profile image
        },
      }));
    });
  }
}
