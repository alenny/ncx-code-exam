import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthRequest } from './auth.request';
import { User } from './user';
import { RegisterRequest } from './register.request';

const currentUserKey = "currentUser";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const authUrl = 'http://localhost:6100/api/authentication';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient
  ) { }

  getCurrentUser(): User {
    let txt = localStorage.getItem(currentUserKey);
    return txt ? JSON.parse(txt) : undefined;
  }

  signin(userName: string, password: string): Observable<User> {
    let req = new AuthRequest();
    req.userName = userName;
    req.password = password;
    return this.http.post<User>(authUrl, req, httpOptions)
      .pipe(
        tap(u => {
          if (u) {
            localStorage.setItem(currentUserKey, JSON.stringify(u));
          }
        })
      )
  }

  signout() {
    localStorage.removeItem(currentUserKey);
  }

  register(userName: string, password: string): Observable<User> {
    let req = new RegisterRequest();
    req.userName = userName;
    req.password = password;
    return this.http.post<User>(`${authUrl}/register`, req, httpOptions)
      .pipe(
        tap(u => {
          if (u) {
            localStorage.setItem(currentUserKey, JSON.stringify(u));
          }
        })
      )
  }
}
