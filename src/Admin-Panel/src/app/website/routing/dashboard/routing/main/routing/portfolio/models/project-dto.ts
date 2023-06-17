import { Tag } from "../../../models";

export class ProjectDto {
  constructor(
    public id: number = -1,
    public title: string = '',
    public link: string = '',
    public tags: Array<Tag> = [],
    public image: FormData = new FormData()
  ) { }
}