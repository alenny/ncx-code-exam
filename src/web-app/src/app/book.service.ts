import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Book } from './book';
import { AuthService } from './auth.service';
import { GetAllBooksResponse } from './getAllBooksResponse';
import { User } from './user';

const bookUrl = 'http://localhost:6100/api/books';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(
    private authService: AuthService,
    private http: HttpClient) { }

  getAllBooks(): Observable<Book[]> {
    let user = this.authService.getCurrentUser();
    if (!user) {
      return of([]);
    }
    return this.http.get<GetAllBooksResponse>(bookUrl, this.getHttpOptions(user))
      .pipe(
        map(res => res.books),
        catchError(this.handleError<Book[]>(`getAllBooks user.id=${user.id}`))
      );
  }

  getBookById(id: number): Observable<Book> {
    let user = this.authService.getCurrentUser();
    if (!user) {
      return of(undefined);
    }
    return this.http.get<Book>(`${bookUrl}/${id}`, this.getHttpOptions(user))
      .pipe(
        catchError(this.handleError<Book>(`getBookById id=${id}`))
      );
  }

  updateBook(book: Book): Observable<any> {
    let user = this.authService.getCurrentUser();
    if (!user) {
      return of(undefined);
    }
    return this.http.put(`${bookUrl}/${book.id}`, book, this.getHttpOptions(user))
      .pipe(
        catchError(this.handleError(`updateBook id=${book.id}`))
      );
  }

  createBook(book: Book): Observable<any> {
    let user = this.authService.getCurrentUser();
    if (!user) {
      return of(undefined);
    }
    return this.http.post(bookUrl, book, this.getHttpOptions(user))
      .pipe(
        catchError(this.handleError(`createBook title=${book.title}`))
      );
  }

  deleteBook(id: number): Observable<any> {
    let user = this.authService.getCurrentUser();
    if (!user) {
      return of(undefined);
    }
    return this.http.delete(`${bookUrl}/${id}`, this.getHttpOptions(user))
      .pipe(
        catchError(this.handleError(`deleteBook id=${id}`))
      );
  }

  private getHttpOptions(user: User) {
    return {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${user.jwtToken}`,
        'Content-Type': 'application/json'
      })
    };
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      // TODO: log error
      return of(result as T);
    };
  }
}
