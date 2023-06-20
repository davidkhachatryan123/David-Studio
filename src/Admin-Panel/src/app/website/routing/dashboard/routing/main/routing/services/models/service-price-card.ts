export class ServicePriceCard {
  constructor(
    public title: string,
    public subtitle: string,
    public icon: string,
    public pricePropertyName: string,
    public editEvent: (property: string) => void
  ) { }
}