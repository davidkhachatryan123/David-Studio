import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: "web"
  },
  {
    path: 'web',
    loadChildren: () => import('./routing/web/web.module').then(module => module.WebModule),
  },
  {
    path: 'desktop',
    loadChildren: () => import('./routing/desktop/desktop.module').then(module => module.DesktopModule),
  },
  {
    path: 'arduino',
    loadChildren: () => import('./routing/arduino/arduino.module').then(module => module.ArduinoModule),
  },
  {
    path: 'hosting',
    loadChildren: () => import('./routing/hosting/hosting.module').then(module => module.HostingModule),
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServicesRoutingModule { }
