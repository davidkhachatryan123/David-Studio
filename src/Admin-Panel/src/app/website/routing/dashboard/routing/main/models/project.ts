import { Tag } from "./tag";

export class Project {
  constructor(
    public id: number = -1,
    public title: string = '',
    public img_uri: string = '',
    public demo_link: string = '',
    public tags: Array<Tag> = []
  ) { }
}