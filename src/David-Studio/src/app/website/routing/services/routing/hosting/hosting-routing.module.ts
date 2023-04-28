import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HostingComponent } from './components/hosting.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HostingComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServicesHostingRoutingModule { }
