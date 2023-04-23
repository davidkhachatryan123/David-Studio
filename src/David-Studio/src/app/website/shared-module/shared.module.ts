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

@NgModule({
  imports: [ RouterModule, CommonModule, TranslateModule ],
  declarations: [ IntroComponent,
                  ContentIntroComponent,
                  NavbarComponent,
                  FooterComponent,
                  PhoneComponent ],
  exports: [ IntroComponent,
             ContentIntroComponent,
             NavbarComponent,
             FooterComponent,
             PhoneComponent,
             TranslateModule ],
})
export class SharedModule { }
