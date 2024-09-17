import { Component, EventEmitter, Input, Output } from '@angular/core';
import { environment } from 'environment';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent {
  
  constructor(private postService: PostService,private authService: AuthService) {}
  @Input() author:any;
  url = environment.url
  postContent: string = '';
  selectedFile: File | null = null;
  imageFile:any;
  imageSrc: string | ArrayBuffer | null = null; 
  @Output() newPost = new EventEmitter<any>();


   onCreatePost(data: any) {
    const formData = new FormData();
    if (this.imageFile)
      formData.append('imageFile', this.imageFile, this.imageFile.name);
    formData.append('content', data.content);    
    this.postService.createPost(formData).subscribe(
      res => {
        res['author'] = this.authService.getAuthor();
        res['comments'] = []
        res['reactions'] = []
        res['createdAt'] = 'now'
        this.newPost.emit(res);
      },
      error => {
        console.error('error', error);
      }
    );
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
    this.imageFile = file;
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
