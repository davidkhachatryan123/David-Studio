export class ConfirmEmailRequestDto {
  constructor(
    public userId: string,
    public returnUrl: string
  ) { }
}