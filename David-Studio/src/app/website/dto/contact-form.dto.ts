export class ContactFormDto {
  constructor(
    public fullName: string,
    public email: string,
    public phoneNumber: string = null,
    public body: string
  ) { }
}