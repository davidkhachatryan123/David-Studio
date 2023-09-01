export class MessageListItemDto {
  constructor(
    public id: number,
    public fullName: string,
    public email: string,
    public phoneNumber: string = null,
    public body: string,
    public isReaded: boolean,
    public hasAnswer: boolean,
    public sentDate: string,
  ) { }
}