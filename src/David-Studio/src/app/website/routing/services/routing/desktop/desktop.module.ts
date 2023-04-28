import { NgModule } from '@angular/core';

import { ServicesDesktopRoutingModule } from './desktop-routing.module';

import { DesktopComponent } from './components/desktop.component';
import { SharedModule } from 'src/app/website/shared-module/shared.module';

@NgModule({
  imports: [ ServicesDesktopRoutingModule,
             SharedModule ],
  declarations: [ DesktopComponent ],
})
export class DesktopModule { }
