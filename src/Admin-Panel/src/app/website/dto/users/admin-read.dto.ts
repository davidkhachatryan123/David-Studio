export class AdminReadDto {
  constructor(
    public id: number,
    public username: string,
    public email: string,
    public phoneNumber: string = null
  ) { }
}