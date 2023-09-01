import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { PageData } from '../../models';
import { ProjectCreateDto, ProjectReadDto } from '../../dto';

@Injectable()
export class ProjectsService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/portfolio/projects`;
  }

  getAll(tableOptions: TableOptions) {
    let queryParams = new HttpParams()
    .append('page', tableOptions.pageIndex + 1)
    .append('size', tableOptions.pageSize)
    .append('orderBy', `${tableOptions.sort} ${tableOptions.sortDirection}`);

    return this.http.get<PageData<ProjectReadDto>>(this.apiUrl, { params: queryParams });
  }

  getById(id: number) {
    return this.http.get<ProjectReadDto>(`${this.apiUrl}/${id}`);
  }

  create(project: ProjectCreateDto) {
    return this.http.post<ProjectReadDto>(
      this.apiUrl,
      this.getFormDataFromProject(project)
    );
  }

  update(id: number, project: ProjectCreateDto) {
    return this.http.put<ProjectReadDto>(
      `${this.apiUrl}/${id}`,
      this.getFormDataFromProject(project)
    );
  }

  delete(id: number) {
    return this.http.delete<ProjectReadDto>(`${this.apiUrl}/${id}`);
  }

  private getFormDataFromProject(project: ProjectCreateDto) {
    let formData = new FormData();
    formData.append('name', project.name);
    formData.append('link', project.link);
    formData.append('file', project.file);
    formData.append('tags', JSON.stringify(project.tags));

    return formData;
  }
}