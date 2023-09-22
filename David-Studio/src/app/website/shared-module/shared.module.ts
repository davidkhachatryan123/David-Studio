import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import {
  IntroComponent,
  NavbarComponent,
  FooterComponent,
  PhoneComponent
} from './components';

@NgModule({
  imports: [ RouterModule, CommonModule, TranslateModule ],
  declarations: [
    IntroComponent,
    NavbarComponent,
    FooterComponent,
    PhoneComponent
  ],
  exports: [
    IntroComponent,
    NavbarComponent,
    FooterComponent,
    PhoneComponent,
    TranslateModule
  ],
})
export class SharedModule { }
