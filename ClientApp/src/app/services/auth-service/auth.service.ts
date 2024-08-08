import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Api } from 'src/app/data/api';
import { AuthForm } from 'src/app/data/authForm';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  userId: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  constructor(private http: HttpClient) { }

  login(authForm: AuthForm) {
    return this.http.post(`${Api.api}/${Api.auth}/${Api.login}`, authForm, {withCredentials: true});
  }

  registration(authForm: AuthForm) {
    return this.http.post(`${Api.api}/${Api.auth}/${Api.registration}`, authForm, {withCredentials: true});
  }

  logout() {
    return this.http.post(`${Api.api}/${Api.auth}/${Api.logout}`, {}, {withCredentials: true});
  }

  isAuthenticatedQuery() {
    return this.http.get(`${Api.api}/${Api.auth}/${Api.isAuthenticated}`, {withCredentials: true});
  }

  isAdminQuery() {
    return this.http.get(`${Api.api}/${Api.auth}/${Api.isAdmin}`, {withCredentials: true});
  }

  userIdQuery() {
    return this.http.get(`${Api.api}/${Api.auth}/${Api.userId}`, { withCredentials: true });
  }
}
