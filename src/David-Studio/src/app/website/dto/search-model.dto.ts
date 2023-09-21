export class SearchModelDto {
  constructor(
    public page: number,
    public count: number,
    public searchText: string = '',
    public tagIds: Array<number> = [],
    public tagsLimit: number = 25,
  ) {}
}