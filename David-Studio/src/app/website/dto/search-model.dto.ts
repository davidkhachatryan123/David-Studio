export class SearchModelDto {
  constructor(
    public page: number,
    public count: number,
    public tagsLimit: number = 25,
    public searchText: string = '',
    public tagIds: Array<number> = [],
  ) {}
}