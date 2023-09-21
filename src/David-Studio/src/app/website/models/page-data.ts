export class PageData<T> {
  constructor(
    public entities: Array<T>,
    public totalCount: number
  ) { }
}