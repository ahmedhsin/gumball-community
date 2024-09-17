import { Component } from '@angular/core';
import { environment } from 'environment';
import { AuthorService } from 'src/app/services/author.service';

@Component({
  selector: 'app-chats-box',
  templateUrl: './chats-box.component.html',
  styleUrls: ['./chats-box.component.css']
})
export class ChatsBoxComponent {
  authors:any = []
  constructor(private authorService: AuthorService){}
  url = environment.url;
  ngOnInit(): void {
    this.authorService.getAuthors().subscribe((res: any) => {
      this.authors = [...res]
      console.log(res);
    })
    
  }
}
