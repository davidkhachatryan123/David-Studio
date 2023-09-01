export class ServicesPricingReadDto {
  constructor(
    public id: number,
    public economPrice: number,
    public standartPrice: number,
    public premiumPlusPrice: number,
    public changeDate: string
  ) { }
}