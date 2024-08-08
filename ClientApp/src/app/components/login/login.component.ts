import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { delay } from 'rxjs';
import { Api } from 'src/app/data/api';
import { AuthForm } from 'src/app/data/authForm';
import { Constants } from 'src/app/data/constants';
import { Message } from 'src/app/data/message';
import { AuthService } from 'src/app/services/auth-service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  title: string = Constants.login;
  alternativeTitle: string = Constants.register;

  authForm: AuthForm = new AuthForm();

  isAuthenticated: boolean = false;

  constructor(private authService: AuthService, private router: Router) {
    this.authService.isAuthenticated.subscribe(
      isAuthenticated => {
        this.isAuthenticated = isAuthenticated

        if (this.isAuthenticated) {
          this.router.navigate(['']);
        }
      }
    );
  }

  changeAction() {
    let temp = this.title;
    this.title = this.alternativeTitle;
    this.alternativeTitle = temp;
  }

  action() {
    if (this.title == Constants.login) {
      this.login();
    }
    else if (this.title == Constants.register) {
      this.registration();
    }
  }

  login() {
    this.authService.login(this.authForm).subscribe(
      ok => {
        this.authService.isAuthenticated.next(true);
        this.authService.isAdminQuery().subscribe(
          isAdmin => this.authService.isAdmin.next(isAdmin as boolean)
        );

        alert(Message.getString(ok as Message));
      },
      error => alert(Message.getString(error.error as Message))
    );
  }

  registration() {
    this.authService.registration(this.authForm).subscribe(
      ok => {
        this.authService.isAuthenticated.next(true);
        alert(Message.getString(ok as Message));
      },
      error => alert(Message.getString(error.error as Message))
    );
  }
}
