import { Component, OnInit } from '@angular/core';
import { Book } from '../book';
import { BookService } from '../book.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  book: Book;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService
  ) { }

  ngOnInit() {
    this.getBook();
  }

  getBook() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.bookService.getBookById(id)
      .subscribe(b => {
        this.book = b;
      });
  }

  updateBook() {
    this.bookService.updateBook(this.book)
      .subscribe(_ => { alert('Update succeeded.') });
  }

  resetBook() {
    this.getBook();
  }

  onPublishDateChange($event) {
    this.book.publishDate = new Date($event.target.value);
  }
}
