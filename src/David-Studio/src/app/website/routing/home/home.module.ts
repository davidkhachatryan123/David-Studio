import { NgModule } from '@angular/core';

import { HomeComponent } from './home.component';

import { SharedModule } from '../../shared-module/shared.module';

import { ServicesComponent } from './components/services/services.component';
import { LatestProjectsComponent } from './components/latest-projects/latest-projects.component';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  imports: [ HomeRoutingModule,
             SharedModule ],
  declarations: [ HomeComponent,
                  ServicesComponent,
                  LatestProjectsComponent ],
})
export class HomeModule { }
