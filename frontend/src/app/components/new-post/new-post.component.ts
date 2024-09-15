import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent {
  @Input() user: any;
  @Output() newPost = new EventEmitter<any>();

  postContent: string = '';
  selectedFile: File | null = null;
  imageSrc: string | ArrayBuffer | null = null;

  onCreatePost(formValue: any) {
    const postData = {
      content: this.postContent,
      image: this.imageSrc,
      user: this.user
    };

    this.newPost.emit(postData);
    this.resetForm();
  }

  resetForm() {
    this.postContent = '';
    this.selectedFile = null;
    this.imageSrc = null;

    const fileInput = document.getElementById('file-input') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
    const imagePreview = document.getElementById('image-preview') as HTMLImageElement;
    if (imagePreview) {
      imagePreview.classList.add('d-none');
    }
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imageSrc = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }
}
