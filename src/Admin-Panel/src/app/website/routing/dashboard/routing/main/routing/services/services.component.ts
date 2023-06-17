import { Component, OnInit } from '@angular/core';
import { ServicePrice } from './models';
import { ServicePriceCard } from './models/service-price-card';
import { EditPriceDialogService } from './services/edit-price-dialog.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-dashboard-main-services',
  templateUrl: 'services.component.html',
  styleUrls: ['services.component.css']
})
export class ServicesComponent implements OnInit {
  prices: Array<ServicePrice>;
  servicePriceCards: Array<ServicePriceCard>;

  constructor(
    private editPriceDialog: EditPriceDialogService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    // get it from api
    this.prices = [
      new ServicePrice(1, 500),
      new ServicePrice(2, 1000),
      new ServicePrice(3, 2000)
    ];

    this.servicePriceCards = [
      new ServicePriceCard('ECONOM', 'You can specify pricing', 'money',
                           this.prices[0], id => this.edit(id)),
      new ServicePriceCard('STANDART', 'You can specify pricing', 'monetization_on',
                           this.prices[1], id => this.edit(id)),
      new ServicePriceCard('PREMIUM+', 'You can specify pricing', 'diamond',
                           this.prices[2], id => this.edit(id))
    ];
  }

  edit(id: number) {
    let price = this.prices.find(price => price.id === id);

    this.editPriceDialog.show(price.value)
    ?.afterClosed().subscribe((result: string) => {
      if(result) {
        price.value = parseInt(result);
        this.snackBar.open('Project price was updated', 'Ok', { duration: 5000 });

        console.log('Prices after change: ', this.prices);
      }
    });
  }
}
