import { TagReadDto } from "./tag-read.dto";

export class SearchSuggestionsDto {
  constructor(
    public projectNames: Array<string>,
    public tags: Array<TagReadDto>
  ) {}
}