import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { NotFoundComponent } from './not-found.component';

@NgModule({
  imports: [ RouterModule.forChild([
    {
      path: '',
      pathMatch: 'full',
      component: NotFoundComponent
    }
  ]),
            TranslateModule ],
  declarations: [ NotFoundComponent ],
})
export class NotFoundModule { }