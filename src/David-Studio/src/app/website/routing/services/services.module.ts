import { NgModule } from '@angular/core';

import { ServicesRoutingModule } from './services-routing.module';
import { SharedModule } from '../../shared-module/shared.module';
import { ServicesComponent } from './services.component';

@NgModule({
  imports: [
    ServicesRoutingModule,
    SharedModule
  ],
  declarations: [
    ServicesComponent
  ]
})
export class ServicesModule { }
