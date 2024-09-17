import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + '/authors';
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<any>(localStorage.getItem('currentUser'));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password })
      .pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        console.log(user);
        return user;
      }));
  }

  siginUp(data:any) {
    return this.http.post<any>(`${this.apiUrl}`, data)
      .pipe(map(user => {
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']); 
  }

  public get isAuthenticated(): boolean {
    return !!localStorage.getItem('currentUser');
  }

  getToken(): string | null {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    return currentUser?.token;
  }

  getAuthor(): any {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    return currentUser?.author;
  }
  getFollowers(): any {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    return currentUser?.followers;
  }

  getFollowing(): any {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    return currentUser?.following;
  }
}
