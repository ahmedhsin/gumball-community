import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {
  @Input() post: any = {}
  
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
    comments.push({
      img: 'assets/images/profile.png',
      name: 'Imposter John',
      comment: comment,
      date: '3 hours ago',
      subComments: [],
      showSubComments: false,
      showReplyForm: false
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
    
  }
  
  isEditing: boolean = false;

  handleUpdate(updatedPost: any) {
    if (updatedPost) {
      this.post = updatedPost;
    }
    this.isEditing = false; // Hide the edit form
  }

  startEditing(post: any) {
    this.post = post;
    this.isEditing = true;
  }
}
