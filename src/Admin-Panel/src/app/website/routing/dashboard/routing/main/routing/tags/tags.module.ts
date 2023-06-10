import { NgModule } from '@angular/core';

import { TagsComponent } from './tags.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: TagsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [],
  declarations: [TagsComponent],
})
export class TagsModule { }
