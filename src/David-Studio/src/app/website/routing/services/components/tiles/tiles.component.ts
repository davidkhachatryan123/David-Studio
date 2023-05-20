import { Component } from '@angular/core';
import { Tile } from './models';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'services-tiles',
  templateUrl: 'tiles.component.html',
  styleUrls: [ 'tiles.component.css' ]
})

export class TilesComponent {
  translateSectionName = 'services';
  tilesdescriptionListTranslationPath = 'template.services.shared.tiles.description_list';

  appRoutes: typeof AppRoutes = AppRoutes;

  econom =  new Tile(
    'Static website',
    500,
    [
      `${this.tilesdescriptionListTranslationPath}.budget`,
      `${this.tilesdescriptionListTranslationPath}.static_website`,
      `${this.tilesdescriptionListTranslationPath}.one_month_support`
    ]
  );

  standart = new Tile(
    'Full-stack',
    1000,
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
    2000,
    [
      `${this.tilesdescriptionListTranslationPath}.with_standart`,
      `${this.tilesdescriptionListTranslationPath}.payments`,
      `${this.tilesdescriptionListTranslationPath}.lts_month_support`
    ]
  );
}