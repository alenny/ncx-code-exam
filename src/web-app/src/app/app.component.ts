import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { User } from './user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'My GoodReads';

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  getCurrentUser(): User {
    return this.authService.getCurrentUser();
  }

  onSignout() {
    this.authService.signout();
    this.router.navigate(['/signin']);
  }
}
