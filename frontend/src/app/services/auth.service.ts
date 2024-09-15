import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://api.escuelajs.co/api/v1/auth'; // Your API endpoint
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<any>(localStorage.getItem('currentUser'));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // Login method
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
    return this.http.post<any>(`${this.apiUrl}/signup`, data)
      .pipe(map(user => {
        return user;
      }));
  }

  // Logout method
  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']); // redirect to login page
  }

  // Check if user is authenticated
  public get isAuthenticated(): boolean {
    return !!localStorage.getItem('currentUser');
  }

  // Get token
  getToken(): string | null {
    const currentUser = JSON.parse(localStorage.getItem('currentUser')!);
    return currentUser?.token;
  }
}
