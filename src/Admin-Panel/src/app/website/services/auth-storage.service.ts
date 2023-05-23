import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { delay, Subscription, of } from 'rxjs';

import { environment } from 'src/environments/environment';

import { AppUser } from '../models';
import { AppRoutes } from '../consts';

@Injectable({
  providedIn: 'root'
})
export class AuthStorageService {
  tokenSubscription = new Subscription();
  timeout: number;

  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  public saveToken(token: string) {
    this.timeout = this.jwtHelper.getTokenExpirationDate(token).valueOf() - new Date().valueOf();
    this.expirationCounter(this.timeout);

    window.localStorage.removeItem(environment.authConfig.TOKEN_KEY);
    window.localStorage.setItem(
      environment.authConfig.TOKEN_KEY,
      token);
  }

  public static getToken(): string {
    return window.localStorage.getItem(environment.authConfig.TOKEN_KEY);
  }

  public saveUser(user: AppUser): void {
    window.localStorage.removeItem(environment.authConfig.USER_KEY);
    window.localStorage.setItem(
      environment.authConfig.USER_KEY,
      JSON.stringify(user));
  }

  public getUser(): AppUser {
    const user = window.localStorage.getItem(environment.authConfig.USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return null;
  }

  public isLoggedIn(): boolean {
    const user = window.localStorage.getItem(environment.authConfig.USER_KEY);
    const token = window.localStorage.getItem(environment.authConfig.TOKEN_KEY);

    if (user && !this.jwtHelper.isTokenExpired(token))
      return true;

    this.clean();
    return false;
  }


  private expirationCounter(timeout) {
    this.tokenSubscription.unsubscribe();
    this.tokenSubscription = of(null).pipe(delay(timeout)).subscribe(() => {
      console.log('EXPIRED!');

      this.clean();
      this.router.navigate([AppRoutes.AUTH, AppRoutes.AUTH_LOGIN]);
    });
  }

  clean(): void {
    window.localStorage.clear();
  }
}