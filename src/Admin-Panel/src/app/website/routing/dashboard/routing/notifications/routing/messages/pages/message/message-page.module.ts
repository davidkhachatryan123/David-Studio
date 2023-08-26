import { NgModule } from '@angular/core';

import { MessagePageComponent } from './message-page.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { MessageComponent } from './components/message/message.component';
import { AnswerComponent } from './components/answer/answer.component';
import { AnswerFieldComponent } from './components/answer-field/answer-field.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: MessagePageComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    SharedModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    MessagePageComponent,
    MessageComponent,
    AnswerComponent,
    AnswerFieldComponent
  ],
})
export class MessagePageModule { }
