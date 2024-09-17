import { Component, Input } from '@angular/core';
import { environment } from 'environment';
import { AuthService } from 'src/app/services/auth.service';
import { AuthorService } from 'src/app/services/author.service';

@Component({
  selector: 'app-profile-header',
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.css']
})
export class ProfileHeaderComponent {
  @Input() author:any;
  @Input() isFollower: any = false;
  currentAuthor = this.authService.getAuthor()
  url = environment.url
  constructor(private authorService: AuthorService, private authService: AuthService){}
  follow(id:any){
    this.authorService.follow(id).subscribe((res)=> {
      this.isFollower = !this.isFollower;
    })
  }

  unFollow(id:any){
    this.authorService.unFollow(id).subscribe((res)=> {
      this.isFollower = !this.isFollower;
    })
  }
}
