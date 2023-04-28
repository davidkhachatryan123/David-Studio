import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArduinoComponent } from './components/arduino.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ArduinoComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServicesArduinoRoutingModule { }
