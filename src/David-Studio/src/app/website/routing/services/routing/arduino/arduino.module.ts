import { NgModule } from '@angular/core';

import { ServicesArduinoRoutingModule } from './arduino-routing.module';

import { ArduinoComponent } from './components/arduino.component';
import { SharedModule } from 'src/app/website/shared-module/shared.module';

@NgModule({
  imports: [ ServicesArduinoRoutingModule,
             SharedModule ],
  declarations: [ ArduinoComponent ],
})
export class ArduinoModule { }
