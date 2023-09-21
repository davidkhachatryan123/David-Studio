export class SearchSuggestionsRequestDto {
  constructor(
    public searchText: string,
    public maxProjects: number,
    public maxTags: number
  ) {}
}