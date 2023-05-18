import { NgModule } from '@angular/core';

import { ServicesRoutingModule } from './services-routing.module';
import { ServicesComponent } from './services.component';
import { SharedModule } from '../../shared-module/shared.module';

import { TilesComponent } from './components/tiles/tiles.component';
import { CirclesComponent } from './components/circles/circles.component';
import { CommonModule } from '@angular/common';

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
  ]
})
export class ServicesModule { }
