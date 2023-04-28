import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import {
  IntroComponent,
  ContentIntroComponent,
  NavbarComponent,
  FooterComponent,
  PhoneComponent
} from './components';
import { TilesComponent } from './components/services/tiles/tiles.component';
import { CirclesComponent } from './components/services/circles/circles.component';

@NgModule({
  imports: [ RouterModule, CommonModule, TranslateModule ],
  declarations: [ IntroComponent,
                  ContentIntroComponent,
                  NavbarComponent,
                  FooterComponent,
                  PhoneComponent,
                  TilesComponent,
                  CirclesComponent ],
  exports: [ IntroComponent,
             ContentIntroComponent,
             NavbarComponent,
             FooterComponent,
             PhoneComponent,
             TranslateModule,
             TilesComponent,
             CirclesComponent ],
})
export class SharedModule { }
