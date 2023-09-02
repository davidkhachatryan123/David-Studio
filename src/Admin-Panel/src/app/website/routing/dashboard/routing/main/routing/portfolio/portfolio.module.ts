import { NgModule } from '@angular/core';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { PortfolioComponent } from './portfolio.component';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ProjectComponent } from './components/projects/project/project.component';
import { TagsComponent } from './components/tags/tags.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { TopComponent } from './components/top/top.component';
import { TagDialogComponent } from './dialogs/tag/tag-dialog.component';
import { TagDialogService } from './services/tag-dialog.service';
import { TopProjectComponent } from './components/top/top-project/top-project.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { SetupProjectWizardComponent } from './wizards/setup-project-wizard/setup-project-wizard.component';
import { ImageCropDialogComponent } from './wizards/dialogs/image-crop/image-crop-dialog.component';
import { ImageCropDialogService } from './wizards/services/image-crop-dialog.service';
import { SetupProjectWizardService } from './wizards/services/setup-project-wizard.service';
import { ProjectsService, TagsService, TopProjectsService } from 'src/app/website/services';
import { PortfolioRoutingModule } from './portfolio-routing.module';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PortfolioRoutingModule,
    SharedModule,
    DragDropModule,
    ImageCropperModule
  ],
  declarations: [
    PortfolioComponent,
    ProjectsComponent,
    ProjectComponent,
    TagsComponent,
    TopComponent,
    TopProjectComponent,
    TagDialogComponent,
    SetupProjectWizardComponent,
    ImageCropDialogComponent
  ],
  providers: [
    TagDialogService,
    ImageCropDialogService,
    SetupProjectWizardService,
    ProjectsService,
    TagsService,
    TopProjectsService
  ]
})
export class PortfolioModule { }
