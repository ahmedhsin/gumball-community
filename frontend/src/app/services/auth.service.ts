import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = ''; // Replace with your backend API URL

  constructor(private http: HttpClient) {}
  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { username, password });
  }

  signup(username:string ,Firstname :string,Lastname :string,Email:string, password:string): Observable<any> {
    return this.http.post(`${this.apiUrl}/signup`, { username,Firstname,Lastname,Email, password });
  }

   // Get basic author data
   getUser(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/users/${id}`);
  }
  // Get detailed user data
  getUserDetails(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/users/${id}`);
  }
  getPosts(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/posts/${id}`);
  }
  }
