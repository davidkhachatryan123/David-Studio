import { TagReadDto } from "src/app/website/dto";

export class Search {
  public constructor(
    public text: string,
    public tags: Array<TagReadDto>,
    public page: number
  ) { }
}