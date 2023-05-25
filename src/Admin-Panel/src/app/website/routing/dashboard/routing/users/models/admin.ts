export class Admin {
  public constructor(
    public id: string,
    public username: string,
    public email: string,
    public emailConfirmed: boolean,
    public phoneNumber: string | null
  ) { }
}