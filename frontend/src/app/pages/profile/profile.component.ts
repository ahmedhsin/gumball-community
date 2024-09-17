import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { AuthorService } from 'src/app/services/author.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  posts:any = []
  author:any = {
    profileImageUrl:'profile.png',
    name:"loading",
    email:"loading"
  };
  isFollower:any;
  constructor(private authService:AuthService,private route: ActivatedRoute, private postService:PostService, private authorService: AuthorService){}
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.postService.getPostsByAuthor(params.get('id')).subscribe((res: any) => {
        this.posts = [...res]
      })
      this.authorService.getAuthor(params.get('id')).subscribe((res)=>{
        this.author = res;
        this.authorService.isFollow(this.author.id).subscribe((res)=>{
          this.isFollower = res
        })
        
      })
    });
    
  }
}
