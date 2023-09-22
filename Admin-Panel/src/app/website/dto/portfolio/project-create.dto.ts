import { TagReadDto } from "./tag-read.dto";

export class ProjectCreateDto {
  constructor(
    public name: string,
    public link: string,
    public file: File,
    public tags: Array<TagReadDto>
  ) { }
}