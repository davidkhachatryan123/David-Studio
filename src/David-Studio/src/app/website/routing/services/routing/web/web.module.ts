import { NgModule } from '@angular/core';

import { ServicesWebRoutingModule } from './web-routing.module';

import { WebComponent } from './components/web.component';
import { SharedModule } from 'src/app/website/shared-module/shared.module';

@NgModule({
  imports: [ ServicesWebRoutingModule,
             SharedModule ],
  declarations: [ WebComponent ],
})
export class WebModule { }
