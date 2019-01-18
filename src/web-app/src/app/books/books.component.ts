import { Component, OnInit } from '@angular/core';
import { Book } from '../book';
import { BookService } from '../book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  books: Book[] = [];

  constructor(
    private bookService: BookService
  ) { }

  ngOnInit() {
    this.getAllBooks();
  }

  getAllBooks() {
    this.bookService.getAllBooks()
      .subscribe(books => this.books = books);
  }

  deleteBook(id: number) {
    if (!confirm("Sure to delete the book?")) {
      return;
    }
    this.bookService.deleteBook(id)
      .subscribe(_ => this.getAllBooks());
  }
}
