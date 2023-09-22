export class AdminReadDto {
  constructor(
    public id: number,
    public username: string,
    public email: string,
    public emailConfirmed: string,
    public phoneNumber: string = null
  ) { }
}