import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ConfirmEmailRequestDto } from '../../dto';

@Injectable()
export class ManageUsersService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/users/manage`;
  }

  sendConfirmationEmail(requestDto: ConfirmEmailRequestDto) {
    return this.http.post(
      `${this.apiUrl}/sendConfirmationEmail`,
      requestDto
    );
  }
}
