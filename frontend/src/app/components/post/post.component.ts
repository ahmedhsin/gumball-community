import { Component, EventEmitter, Input, Output } from '@angular/core';
import { environment } from 'environment';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {
  constructor(private postService: PostService,private authService: AuthService){}
  @Input() post: any = {}
  author = this.authService.getAuthor();
  url = environment.url
  isReactVisible = false;
  isCommentsVisible = false;
  toggleReact() {
    this.isReactVisible = !this.isReactVisible;
  }
  toggleSubComments(comment: any) {
    comment.showSubComments = !comment.showSubComments;
  }

  toggleComments(){
    this.isCommentsVisible = !this.isCommentsVisible;
  }

  addComment(comments:any, comment: any){
    this.postService.addComment({
      postId: this.post.id,
      content: comment
    }).subscribe((res: any) => {
      comments.push({
        ...res,
        author: this.authService.getAuthor(),
        subComments: [],
        showSubComments: false,
        showReplyForm: false
      })
    })
    
  }
  setReact(react: any){
    if(this.post.react){
      this.post.reactions[this.post.react] -= 1;
    }
    if (this.post.react == react){
      this.post.react = null;
    }else{
      this.post.react = react;
      this.post.reactions[react] += 1;
    }
    
    this.postService.addReact(react, this.post.id).subscribe((res: any) => {})    
  }
  
  isEditing: boolean = false;

  handleUpdate(updatedPost: any) {
    console.log(updatedPost)
    if (updatedPost) {
      if (updatedPost == 'delete'){
        this.postService.deletePost(this.post.id).subscribe((res)=>{
          document.querySelector('.post-'+this.post.id)?.classList.add('d-none');
        })
      }else{
        this.postService.updatePost(this.post.id, {
          content:updatedPost.content
        }).subscribe((res)=>{
          this.post.content = updatedPost.content;
          this.post.createdAt = 'now'
        })
      }
    }
    this.isEditing = false;
  }

  startEditing(post: any) {
    this.post = post;
    this.isEditing = true;
  }
}
