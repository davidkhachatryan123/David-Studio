export class ServicesPricingCreateDto {
  constructor(
    public economPrice: number,
    public standartPrice: number,
    public premiumPlusPrice: number
  ) { }
}