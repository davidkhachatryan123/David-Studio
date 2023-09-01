import { TagReadDto } from "./tag-read.dto";

export class ProjectReadDto {
  constructor(
    public id: number,
    public name: string,
    public link: string,
    public imageUrl: string,
    public tags: Array<TagReadDto>
  ) { }
}