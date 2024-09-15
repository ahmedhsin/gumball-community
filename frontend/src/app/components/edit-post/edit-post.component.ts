// edit-post.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.css']
})
export class EditPostComponent {
  @Input() post: any; // Input post data to be edited
  @Output() updatePost = new EventEmitter<any>(); // Emit updated post data

  editedContent: string = '';
  editedImage: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;

  ngOnInit() {
    this.editedContent = this.post.content;
  }

  onUpdatePost() {
    const updatedPost = {
      ...this.post,
      content: this.editedContent,
    };

    this.updatePost.emit(updatedPost);
  }

  onCancel() {
    this.updatePost.emit(null);
  }

  onDelete(){
    this.updatePost.emit('delete');
  }
}
