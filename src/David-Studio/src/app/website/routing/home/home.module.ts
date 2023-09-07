import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../../shared-module/shared.module';

import { HomeComponent } from './home.component';
import { ServicesComponent } from './components/service/service.component';
import { TopProjectsComponent } from './components/top-projects/top-projects.component';

import { TopProjectsService } from '../../services';

@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule
  ],
  declarations: [
    HomeComponent,
    ServicesComponent,
    TopProjectsComponent
  ],
  providers: [
    TopProjectsService
  ]
})
export class HomeModule { }
