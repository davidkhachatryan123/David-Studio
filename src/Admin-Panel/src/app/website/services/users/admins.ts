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
    this.apiUrl = `${environment.api}/projects`;
  }

  getAll(tableOptions: TableOptions) {
    let queryParams = new HttpParams()
    .append('page', tableOptions.pageIndex + 1)
    .append('size', tableOptions.pageSize)
    .append('orderBy', `${tableOptions.sort} ${tableOptions.sortDirection}`);

    return this.http.get<PageData<AdminReadDto>>(this.apiUrl, { params: queryParams });
  }

  getById(id: number) {
    let queryParams = new HttpParams()
    .append('id', id);

    return this.http.get<AdminReadDto>(this.apiUrl, { params: queryParams });
  }

  create(admin: AdminCreateDto) {
    return this.http.post<AdminReadDto>(this.apiUrl, admin);
  }

  update(id: number, admin: AdminCreateDto) {
    let queryParams = new HttpParams()
    .append('id', id);

    return this.http.put<AdminReadDto>(this.apiUrl, admin, { params: queryParams });
  }

  delete(id: number) {
    let queryParams = new HttpParams()
    .append('id', id);

    return this.http.delete<AdminReadDto>(this.apiUrl, { params: queryParams });
  }
}