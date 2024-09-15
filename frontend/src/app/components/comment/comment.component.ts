import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent {
  @Input() comment: any = {};

  toggleReplyForm(comment: any) {
    comment.showReplyForm = !comment.showReplyForm;
  }

  addReply(comment: any, replyText: string) {
    comment.subComments.push({
      img: 'assets/images/profile.png',
      name: 'Deo John',
      comment: replyText,
      date: '3 hours ago',
      subComments: [],
      showSubComments: false,
      showReplyForm: false
    });
    comment.showReplyForm = false;
  }

  isEditingComment: boolean = false;
  currentComment: any;

  startEditing(comment: any) {
    this.currentComment = { ...comment }; 
    this.isEditingComment = true;
  }

  handleUpdateComment(updatedComment: any) {
    if (updatedComment === 'delete') {
      document.querySelector('.container')?.classList.add('d-none');
      this.isEditingComment = false;
    }else if (updatedComment) {
      this.comment = updatedComment;
      this.comment.date = 'now';
    }
    this.isEditingComment = false; 
  }

  cancelEdit() {
    this.isEditingComment = false; 
  }
}
