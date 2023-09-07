import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ServicesRoutingModule } from './services-routing.module';
import { SharedModule } from '../../shared-module/shared.module';

import { ServicesComponent } from './services.component';
import { TilesComponent } from './components/tiles/tiles.component';
import { CirclesComponent } from './components/circles/circles.component';

import { PricingService } from '../../services';

@NgModule({
  imports: [
    CommonModule,
    ServicesRoutingModule,
    SharedModule
  ],
  declarations: [
    ServicesComponent,
    TilesComponent,
    CirclesComponent
  ],
  providers: [
    PricingService
  ]
})
export class ServicesModule { }
