import { Component, OnInit } from '@angular/core';
import { ServicesPrice } from './models';
import { ServicePriceCard } from './models/service-price-card';
import { EditPriceDialogService } from './services/edit-price-dialog.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-dashboard-main-services',
  templateUrl: 'services.component.html',
  styleUrls: ['services.component.css']
})
export class ServicesComponent implements OnInit {
  prices: ServicesPrice;
  servicePriceCards: Array<ServicePriceCard>;

  constructor(
    private editPriceDialog: EditPriceDialogService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    // get it from api
    this.prices = new ServicesPrice(1, 500, 1000, 2000);

    this.servicePriceCards = [
      new ServicePriceCard('ECONOM', 'You can specify pricing', 'money',
                           'econom', property => this.edit(property)),
      new ServicePriceCard('STANDART', 'You can specify pricing', 'monetization_on',
                           'standart', property => this.edit(property)),
      new ServicePriceCard('PREMIUM+', 'You can specify pricing', 'diamond',
                           'premiumPlus', property => this.edit(property))
    ];
  }

  edit(propertyName: string) {
    this.editPriceDialog.show(this.prices[propertyName])
    ?.afterClosed().subscribe((result: string) => {
      if(result) {
        this.prices[propertyName] = parseInt(result);
        this.snackBar.open('Project price was updated', 'Ok', { duration: 5000 });

        console.log('Prices after change: ', this.prices);
      }
    });
  }
}
