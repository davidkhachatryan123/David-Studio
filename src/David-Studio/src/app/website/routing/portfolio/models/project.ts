import { Tag } from "./tag";

export class Project {
  public constructor(
    public title: string,
    public tags: Array<Tag>,
    public image: string,
    public href: string
  ) { }
}