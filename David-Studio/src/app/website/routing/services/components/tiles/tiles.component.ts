import { Component, OnInit } from '@angular/core';
import { Tile } from './models';
import { AppRoutes } from 'src/app/website/consts';
import { PricingService } from 'src/app/website/services';
import { ServicesPricingReadDto } from 'src/app/website/dto';

@Component({
  selector: 'services-tiles',
  templateUrl: 'tiles.component.html',
  styleUrls: [ 'tiles.component.css' ]
})

export class TilesComponent implements OnInit {
  translateSectionName = 'services';
  tilesdescriptionListTranslationPath = 'template.services.shared.tiles.description_list';

  appRoutes: typeof AppRoutes = AppRoutes;

  econom = new Tile(
    'Static website',
    0,
    [
      `${this.tilesdescriptionListTranslationPath}.budget`,
      `${this.tilesdescriptionListTranslationPath}.static_website`,
      `${this.tilesdescriptionListTranslationPath}.one_month_support`
    ]
  );

  standart = new Tile(
    'Full-stack',
    0,
    [
      `${this.tilesdescriptionListTranslationPath}.with_econom`,
      `${this.tilesdescriptionListTranslationPath}.admin_panel`,
      `${this.tilesdescriptionListTranslationPath}.auth`,
      `${this.tilesdescriptionListTranslationPath}.db`,
      `${this.tilesdescriptionListTranslationPath}.three_month_support`
    ]
  );

  premiumPlus = new Tile(
    'Full-stack and LTS',
    0,
    [
      `${this.tilesdescriptionListTranslationPath}.with_standart`,
      `${this.tilesdescriptionListTranslationPath}.payments`,
      `${this.tilesdescriptionListTranslationPath}.lts_month_support`
    ]
  );

  constructor(private pricingService: PricingService) { }

  ngOnInit() {
    this.pricingService.getPrices()
    .subscribe((pricices: ServicesPricingReadDto) => {
      this.econom.price = pricices.economPrice;
      this.standart.price = pricices.standartPrice;
      this.premiumPlus.price = pricices.premiumPlusPrice;
    });
  }
}