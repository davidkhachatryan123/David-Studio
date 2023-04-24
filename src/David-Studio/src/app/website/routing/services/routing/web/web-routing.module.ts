import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WebComponent } from './components/web.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: WebComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServicesWebRoutingModule { }
