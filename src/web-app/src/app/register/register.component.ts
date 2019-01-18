import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  userName: string;
  password: string;
  passwordConfirm: string;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  onRegister() {
    if (!this.userName || !this.password || !this.passwordConfirm) {
      alert('Please fill all fields');
      return;
    }
    if (this.password !== this.passwordConfirm) {
      alert('Passwords did not match each other');
      return;
    }
    this.authService.register(this.userName, this.password)
      .subscribe(
        _ => this.router.navigate(['']),
        err => {
          alert('User name exists');
        });
  }
}
