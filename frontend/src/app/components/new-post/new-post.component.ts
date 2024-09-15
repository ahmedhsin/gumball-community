import { Component, EventEmitter, Output } from '@angular/core';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent {
  
  constructor(private postService: PostService) {}
  postContent: string = '';
  selectedFile: File | null = null;
  imageSrc: string | ArrayBuffer | null = null; 
  @Output() newPost = new EventEmitter<any>();
   onCreatePost(data: any) {
    data['image'] = this.imageSrc;
    this.newPost.emit(data);
    /*
    this.postService.createPost(data).subscribe(
      response => {
        console.log('Created successful', response);
      },
      error => {
        console.error('error', error);
      }
    );*/
    this.imageSrc = null;
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
    document.getElementById('image-preview')?.classList.add('d-none');
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
  
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const imagePreview: any = document.getElementById('image-preview');
        imagePreview.src = e.target.result;
        this.imageSrc = e.target.result;
        imagePreview.classList.remove('d-none');
      };
      reader.readAsDataURL(file);
    }
  }
}
