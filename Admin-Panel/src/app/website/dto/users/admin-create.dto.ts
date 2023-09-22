export class AdminCreateDto {
  constructor(
    public username: string,
    public password: string,
    public passwordConfirmation: string,
    public email: string,
    public phoneNumber: string = null
  ) { }
}