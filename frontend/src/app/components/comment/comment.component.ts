import { Component, Input } from '@angular/core';
import { environment } from 'environment';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent {
  @Input() comment: any = {};
  @Input() post: any = {};
  url = environment.url;
  constructor(private postService: PostService,private authService: AuthService){}
  author = this.authService.getAuthor()
  toggleReplyForm(comment: any) {
    comment.showReplyForm = !comment.showReplyForm;
  }

  addReply(comment: any, replyText: string) {

    this.postService.addComment({
      postId: this.post.id,
      content: replyText,
      parentId:comment.id
    }).subscribe((res: any) => {
      res['createdAt'] = 'now';
      comment.subComments.push({
        ...res,
        author: this.authService.getAuthor(),
        subComments: [],
        showSubComments: false,
        showReplyForm: false
      })
    })
  }

  isEditingComment: boolean = false;
  currentComment: any;

  startEditing(comment: any) {
    this.currentComment = { ...comment }; 
    this.isEditingComment = true;
  }

  handleUpdateComment(updatedComment: any) {
    if (updatedComment === 'delete') {
      this.postService.deleteComment(this.comment.id).subscribe((res: any) => {
        document.querySelector('.comment-'+this.comment.id)?.classList.add('d-none');
        this.isEditingComment = false;
      })
    }else if (updatedComment) {
      this.postService.updateComment(this.comment.id, {
        content:updatedComment.content
      }).subscribe((res)=>{
        this.comment = updatedComment;
        this.comment.createdAt = 'now';
      })
    }
    this.isEditingComment = false; 
  }

  cancelEdit() {
    this.isEditingComment = false; 
  }
}
