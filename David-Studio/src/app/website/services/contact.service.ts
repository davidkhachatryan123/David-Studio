import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ContactFormDto } from '../dto';

@Injectable()
export class ContactService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/messenger/contact`;
  }

  sendMessage(contactForm: ContactFormDto) {
    return this.http.post(this.apiUrl, contactForm);
  }
}