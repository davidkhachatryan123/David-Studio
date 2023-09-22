import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { PageData } from '../../models';
import { TagCreateDto, TagReadDto } from '../../dto';

@Injectable()
export class TagsService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/portfolio/tags`;
  }

  getAll(tableOptions: TableOptions) {
    let queryParams = new HttpParams()
    .append('page', tableOptions.pageIndex + 1)
    .append('size', tableOptions.pageSize)
    .append('orderBy', `${tableOptions.sort} ${tableOptions.sortDirection}`);

    return this.http.get<PageData<TagReadDto>>(this.apiUrl, { params: queryParams });
  }

  getById(id: number) {
    return this.http.get<TagReadDto>(`${this.apiUrl}/${id}`);
  }

  create(tag: TagCreateDto) {
    return this.http.post<TagReadDto>(this.apiUrl, tag);
  }

  update(id: number, tag: TagCreateDto) {
    return this.http.put<TagReadDto>(`${this.apiUrl}/${id}`, tag);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}