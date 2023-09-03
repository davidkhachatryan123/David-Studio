export class AnswerDto {
  constructor(
    public id: number,
    public body: string,
    public answeredDate: Date
  ) { }
}