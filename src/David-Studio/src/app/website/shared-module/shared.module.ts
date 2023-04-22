import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import {
  IntroComponent,
  ContentIntroComponent,
  NavbarComponent,
  FooterComponent,
  PhoneComponent
} from './components';

@NgModule({
  imports: [ RouterModule, CommonModule ],
  declarations: [ IntroComponent,
                  ContentIntroComponent,
                  NavbarComponent,
                  FooterComponent,
                  PhoneComponent ],
  exports: [ IntroComponent,
             ContentIntroComponent,
             NavbarComponent,
             FooterComponent,
             PhoneComponent ],
})
export class SharedModule { }
