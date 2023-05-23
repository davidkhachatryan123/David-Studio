import { NgModule } from '@angular/core';

import { ServicesComponent } from './services.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: ServicesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [],
  declarations: [ServicesComponent],
})
export class ServicesModule { }
