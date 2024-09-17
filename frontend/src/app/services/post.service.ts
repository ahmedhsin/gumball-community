import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }
  url = environment.apiUrl + '/posts';
  react_url = environment.apiUrl + '/react'
  comment_url = environment.apiUrl + '/comments'
  getPosts() {
    return this.http.get(this.url)
  }

  getPost(id:any){
    return this.http.get(this.url+'/'+id);
  }
  getPostsByAuthor(id:any){
    return this.http.get(this.url+'/author/'+id);
  }

  createPost(data:any): Observable<any> {
    return this.http.post(this.url, data)
   }

  deletePost(id:any) {
    return this.http.delete(this.url+'/'+id)
  }

  updatePost(id:any, data:any) {
    return this.http.put(this.url+'/'+id, data)
  }

  addComment(data: any) {
    console.log(data)
    return this.http.post(this.comment_url, data)
  }

  deleteComment(id:any) {
    return this.http.delete(this.comment_url+'/'+id)
  }

  updateComment(id:any, data:any) {
    console.log(data)
    return this.http.put(this.comment_url+'/'+id, data)
  }

  addReact(react:any, postId: any) {
    const data = {
      react,
      postId
    }
    console.log(data)
    return this.http.put(this.react_url, data)
  }

}
