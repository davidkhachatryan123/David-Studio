import { NgModule } from '@angular/core';

import { HomeComponent } from './home.component';

import { SharedModule } from '../../shared-module/shared.module';

import { ServicesComponent } from './components/service/service.component';
import { TopProjectsComponent } from './components/top-projects/top-projects.component';
import { HomeRoutingModule } from './home-routing.module';
import { CommonModule } from '@angular/common';

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
})
export class HomeModule { }
