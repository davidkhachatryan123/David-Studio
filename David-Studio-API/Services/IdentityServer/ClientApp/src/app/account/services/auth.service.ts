import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) { }
  
  login(login: any): Observable<{ returnUrl: string, mfa: string, rememberMe: string }> {
    return this.http.post<{returnUrl: string, mfa: string, rememberMe: string}>
      (`${environment.api_url}/account/login`, login, { withCredentials: true });
  }

  mfaLogin(loginMfa: any): Observable<{ returnUrl: string }> {
    return this.http.post<{returnUrl: string}>
      (`${environment.api_url}/account/loginMfa`, loginMfa, { withCredentials: true });
  }

  logout(logoutId: string): Observable<{ prompt: boolean, postLogoutRedirectUri: string }> {
    let queryParams = new HttpParams().set('logoutId', logoutId);

    return this.http.get<{ prompt: boolean, postLogoutRedirectUri: string }>
      (`${environment.api_url}/account/logout`, { params: queryParams, withCredentials: true });
  }

  confirmLogout(logoutId: string): Observable<{ postLogoutRedirectUri: string }> {
    let queryParams = new HttpParams().set('logoutId', logoutId);

    return this.http.post<{ postLogoutRedirectUri: string }>
      (`${environment.api_url}/account/logout`, null, { params: queryParams, withCredentials: true });
  }
}