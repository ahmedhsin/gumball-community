import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  constructor(private http: HttpClient) { }
  url = environment.apiUrl + '/authors';
  url_follow = environment.apiUrl + '/authorfollow'
  getAuthors() {
    return this.http.get(this.url)
  }

  getAuthor(id: any) {
    return this.http.get(this.url+'/'+id)
  }

  follow(id:any){
    return this.http.post(this.url_follow, {
      followingId: id
    })
  }

  isFollow(id:any){
    return this.http.get(this.url_follow+'/isfollowing/'+id)
  }
  unFollow(id:any){
    return this.http.delete(this.url_follow+'/'+id)
  }

  
}
