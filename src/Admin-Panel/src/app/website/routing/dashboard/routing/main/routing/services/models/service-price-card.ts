import { ServicePrice } from "./service-price";

export class ServicePriceCard {
  constructor(
    public title: string,
    public subtitle: string,
    public icon: string,
    public price: ServicePrice,
    public editEvent: (id: number) => void
  ) { }
}