import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProjectReadDto, SearchModelDto, SearchSuggestionsDto, SearchSuggestionsRequestDto } from '../dto';
import { PageData } from '../models';

@Injectable()
export class PortfolioService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/search-engine`;
  }

  search(request: SearchModelDto) {
    let queryParams = new HttpParams()
    .append('page', request.page)
    .append('count', request.count)
    .append('searchText', request.searchText)
    .append('tagsLimit', request.tagsLimit);

    for (let index = 0; index < request.tagIds.length; index++) {
      queryParams = queryParams.append('tagIds', request.tagIds[index]);
    }

    return this.http.get<PageData<ProjectReadDto>>(
      `${this.apiUrl}/search`,
      { params: queryParams }
    );
  }

  getSuggestions(request: SearchSuggestionsRequestDto) {
    let queryParams = new HttpParams()
    .append('searchText', request.searchText)
    .append('maxProjects', request.maxProjects)
    .append('maxTags', request.maxTags);

    return this.http.get<SearchSuggestionsDto>(
      `${this.apiUrl}/search/getSuggestions`,
      { params: queryParams }
    );
  }
}