import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DesktopComponent } from './components/desktop.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: DesktopComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServicesDesktopRoutingModule { }
