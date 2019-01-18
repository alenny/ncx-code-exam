import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  userName: string;
  password: string;

  constructor(
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
  }

  onSignin() {
    this.authService.signin(this.userName, this.password)
      .subscribe(
        _ => this.router.navigate(['']),
        err => {
          alert('Bad credentials');
        });
  }
}
