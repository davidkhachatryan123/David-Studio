export class MessageDto {
  constructor(
    public id: number,
    public fullName: string,
    public email: string,
    public phoneNumber: string = null,
    public body: string,
    public isReaded: boolean,
    public sentDate: string,
  ) { }
}