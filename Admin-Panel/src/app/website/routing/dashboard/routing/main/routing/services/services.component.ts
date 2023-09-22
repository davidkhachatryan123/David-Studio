import { Component, OnInit } from '@angular/core';
import { ServicePriceCard } from './models/service-price-card';
import { EditPriceDialogService } from './services/edit-price-dialog.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ServicesPricingCreateDto, ServicesPricingReadDto } from 'src/app/website/dto';
import { PricingService } from 'src/app/website/services';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-dashboard-main-services',
  templateUrl: 'services.component.html',
  styleUrls: ['services.component.css']
})
export class ServicesComponent implements OnInit {
  prices: ServicesPricingReadDto = new ServicesPricingReadDto(-1, 0, 0, 0, '');
  servicePriceCards: Array<ServicePriceCard>;

  constructor(
    private editPriceDialog: EditPriceDialogService,
    private snackBar: MatSnackBar,
    private pricingService: PricingService
  ) {
    this.servicePriceCards = [
      new ServicePriceCard('ECONOM', 'You can specify pricing', 'money',
                           'economPrice', property => this.edit(property)),
      new ServicePriceCard('STANDART', 'You can specify pricing', 'monetization_on',
                           'standartPrice', property => this.edit(property)),
      new ServicePriceCard('PREMIUM+', 'You can specify pricing', 'diamond',
                           'premiumPlusPrice', property => this.edit(property))
    ];
  }

  ngOnInit() {
    this.reloadData();
  }

  reloadData() {
    this.pricingService.getPrices()
    .subscribe(
      (prices: ServicesPricingReadDto) => this.prices = prices,
      (error : HttpErrorResponse) => {
        if(error.status == HttpStatusCode.NotFound)
          this.snackBar.open('Not found any pricing plans', 'Ok', { duration: 5000 });
      }
    );
  }

  edit(propertyName: string) {
    this.editPriceDialog.show(this.prices[propertyName])
    ?.afterClosed().subscribe((result: string) => {
      if(result) {
        this.prices[propertyName] = parseInt(result);

        this.pricingService.setPrices(new ServicesPricingCreateDto(
          this.prices.economPrice,
          this.prices.standartPrice,
          this.prices.premiumPlusPrice
        )).subscribe(_ => {
          this.reloadData();
          this.snackBar.open('Pricing plan updated', 'Ok', { duration: 5000 });
        });
      }
    });
  }
}
