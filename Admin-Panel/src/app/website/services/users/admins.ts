import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { PageData } from '../../models';
import { AdminCreateDto, AdminReadDto } from '../../dto';

@Injectable()
export class AdminsService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/users/admins`;
  }

  getAll(tableOptions: TableOptions) {
    let queryParams = new HttpParams()
    .append('page', tableOptions.pageIndex + 1)
    .append('size', tableOptions.pageSize)
    .append('orderBy', `${tableOptions.sort} ${tableOptions.sortDirection}`);

    return this.http.get<PageData<AdminReadDto>>(this.apiUrl, { params: queryParams });
  }

  getById(id: number) {
    return this.http.get<AdminReadDto>(`${this.apiUrl}/${id}`);
  }

  create(admin: AdminCreateDto) {
    return this.http.post<AdminReadDto>(this.apiUrl, admin);
  }

  update(id: number, admin: AdminCreateDto) {
    return this.http.put<AdminReadDto>(`${this.apiUrl}/${id}`, admin);
  }

  delete(id: number) {
    return this.http.delete<AdminReadDto>(`${this.apiUrl}/${id}`);
  }
}