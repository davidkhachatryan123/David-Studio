import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { environment } from 'src/environments/environment';
import { PageData } from '../../models';
import { AnswerCreateDto, AnswerDto, MessageDto, MessageListItemDto } from '../../dto';

@Injectable()
export class ContactService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/messenger/contact`;
  }

  getMessagesList(tableOptions: TableOptions) {
    let queryParams = new HttpParams()
    .append('page', tableOptions.pageIndex + 1)
    .append('size', tableOptions.pageSize)
    .append('orderBy', `${tableOptions.sort} ${tableOptions.sortDirection}`);

    return this.http.get<PageData<MessageListItemDto>>(`${this.apiUrl}/getMessagesList`, { params: queryParams });
  }

  readMessage(messageId: number) {
    let queryParams = new HttpParams()
    .append('id', messageId);
  
    return this.http.get<MessageDto>(`${this.apiUrl}/readMessage`, { params: queryParams });
  }

  readAnswer(messageId: number) {
    let queryParams = new HttpParams()
    .append('id', messageId);
  
    return this.http.get<AnswerDto>(`${this.apiUrl}/readAnswer`, { params: queryParams });
  }

  answer(messageId: number, answer: AnswerCreateDto) {
    let queryParams = new HttpParams()
    .append('id', messageId);

    return this.http.post<AnswerDto>(`${this.apiUrl}/answer`, answer, { params: queryParams });
  }
}