import { Component, OnInit } from '@angular/core';
import { Book } from '../book';
import { BookService } from '../book.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  book: Book;

  constructor(
    private router: Router,
    private bookService: BookService
  ) { }

  ngOnInit() {
    this.book = new Book();
    this.book.price = 0.0;
    this.book.publishDate = new Date();
    this.book.rating = 0.0;
  }

  createBook() {
    if (!this.book.title || !this.book.category || this.book.price === undefined || !this.book.publishDate || !this.book.rating === undefined) {
      alert('Please fill all fields');
      return;
    }
    this.bookService.createBook(this.book)
      .subscribe(_ => this.router.navigate(["/"]));
  }

  onPublishDateChange($event) {
    this.book.publishDate = new Date($event.target.value);
  }
}
