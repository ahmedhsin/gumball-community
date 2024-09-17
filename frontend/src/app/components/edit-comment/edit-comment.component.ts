// edit-comment.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css']
})
export class EditCommentComponent {
  @Input() comment: any;
  @Output() updateComment = new EventEmitter<any>();

  editedComment: string = '';

  ngOnInit() {
    this.editedComment = this.comment.content;
  }

  onUpdateComment() {
    const updatedComment = {
      ...this.comment,
      content: this.editedComment
    };
    this.updateComment.emit(updatedComment);
  }

  onCancel() {
    this.updateComment.emit(null);
  }

  onDelete(){
    
    this.updateComment.emit('delete');
  }

}
