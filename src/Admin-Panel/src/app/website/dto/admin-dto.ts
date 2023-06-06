export class AdminDto {
  constructor(
    public id: string = '',
    public username: string = '',
    public password: string = '',
    public passwordConfirm: string = '',
    public email: string = '',
    public phoneNumber: string = ''
  ) { }
}