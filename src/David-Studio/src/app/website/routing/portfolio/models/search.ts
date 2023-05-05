import { Pagintaion } from "./pagination";
import { Tag } from "./tag";

export class Search {
  public constructor(
    public text: string,
    public tags: Array<Tag>,
    public page: number
  ) { }
}