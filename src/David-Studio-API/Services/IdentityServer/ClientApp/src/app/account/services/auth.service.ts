import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class AuthService {
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) { }
  
  login(login: any): Observable<{ returnUrl: string }> {
    console.log('test');
    return this.http.post<{returnUrl: string}>('/api/login', login);
  }

  logout(logoutId: string): Observable<{ prompt: boolean, postLogoutRedirectUri: string }> {
    let queryParams = new HttpParams().set('logoutId', logoutId);

    return this.http.get<{ prompt: boolean, postLogoutRedirectUri: string }>
      ('/api/logout', { params: queryParams });
  }

  confirmLogout(logoutId: string): Observable<{ postLogoutRedirectUri: string }> {
    let queryParams = new HttpParams().set('logoutId', logoutId);

    return this.http.post<{ postLogoutRedirectUri: string }>
      ('/api/logout', { params: queryParams });
  }
}