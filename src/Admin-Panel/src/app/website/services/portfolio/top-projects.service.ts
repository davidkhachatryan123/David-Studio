import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProjectReadDto } from '../../dto';

@Injectable()
export class TopProjectsService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/portfolio/topProjects`;
  }

  getAll(limit: number = -1) {
    let queryParams = new HttpParams()
    .append('limit', limit);

    return this.http.get<Array<ProjectReadDto>>(this.apiUrl, { params: queryParams });
  }

  mark(projects: Array<ProjectReadDto>) {
    return this.http.post<Array<number>>(this.apiUrl, projects);
  }

  remove(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  reorder(ids: Array<number>) {
    return this.http.post(this.apiUrl, ids);
  }
}