import { Component, ViewChild, ElementRef, OnInit, AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Observable, map, startWith } from 'rxjs';
import { ImageCropDialogService } from '../services/image-crop-dialog.service';
import { ProjectCreateDto, ProjectReadDto, TagReadDto } from 'src/app/website/dto';
import { ProjectsService, TagsService } from 'src/app/website/services';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { PageData } from 'src/app/website/models';

@Component({
  selector: 'app-dashboard-main-portfolio-setup-project-wizard',
  templateUrl: 'setup-project-wizard.component.html',
  styleUrls: ['setup-project-wizard.component.css']
})
export class SetupProjectWizardComponent implements OnInit, AfterViewInit {
  projectDto: ProjectCreateDto | ProjectReadDto = new ProjectReadDto(null, null, null, null, null);
  projectForm: FormGroup;

  img: any = '';
  imgFile: File;

  allTags: Array<TagReadDto>;
  filteredTags: Observable<Array<string>>;

  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement>;

  constructor(
    private imageCropDialogService: ImageCropDialogService,
    private router: Router,
    private route: ActivatedRoute,
    private projectsService: ProjectsService,
    private tagsService: TagsService
  ) {
    this.projectForm = new FormGroup({
      "name": new FormControl('', [Validators.required, Validators.maxLength(128)]),
      "link": new FormControl('', [Validators.required, Validators.maxLength(512)]),
      "image": new FormControl(''),
      "tagCtrl": new FormControl('')
    });
  }

  ngOnInit () {
    this.route.queryParams.subscribe(params => {
      if (params['id'] != -1) {
        this.projectsService.getById(params['id'])
        .subscribe((project: ProjectReadDto) => {
          this.projectDto = project;
          this.img = project.imageUrl;

          this.projectForm.controls['name'].setValue(this.projectDto.name);
          this.projectForm.controls['link'].setValue(this.projectDto.link);
        });
      } else {
        this.projectDto = new ProjectCreateDto(null, null, null, null);
      }
    });

    this.tagsService.getAll(new TableOptions('id', 'asc', 0, 1))
    .subscribe((tags: PageData<TagReadDto>) => {
      this.allTags = tags.entities

      this.filteredTags = this.projectForm.controls['tagCtrl'].valueChanges.pipe(
        startWith(null),
        map((tag: string | null) =>
        (tag ? this._filterTags(tag) : this.allTags?.map(data => data.name).slice())),
      );
    });
  }

  ngAfterViewInit() {
    this.projectForm.controls['name'].valueChanges
      .subscribe(value => this.projectDto.name = value);
    this.projectForm.controls['link'].valueChanges
      .subscribe(value => this.projectDto.link = value);
  }

  onFileChange(event: Event) {
    this.imageCropDialogService.show(event)
    ?.afterClosed().subscribe((result: any) => {
      if(result)
        this.img = result;
        this.imgFile = ((event.target as HTMLInputElement).files as FileList)[0];
    });
  }

  removeTag(tag: TagReadDto) {
    this.projectDto.tags.includes(tag)
      ? this.projectDto.tags.splice(this.projectDto.tags.indexOf(tag), 1)
      : null;
  }

  tagSelected(event: MatAutocompleteSelectedEvent) {
    if (!this.projectDto.tags.find(tag => tag.name == event.option.value))
      this.projectDto.tags.push(this.allTags.find(tag => tag.name == event.option.value));

    this.tagInput.nativeElement.value = '';
    this.projectForm.controls['tagCtrl'].setValue(null);

    console.log(this.projectDto.tags);
  }

  private _filterTags(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allTags.map(tag => tag.name)
    .filter(tag => tag.toLowerCase().includes(filterValue));
  }

  navigatToReturnUrl() {
    this.route.queryParams.subscribe(params => {
      this.router.navigate([params['returnUrl']]);
    });
  }

  submit() {
    if(this.projectForm.valid) {
      this.route.queryParams.subscribe(params => {
        if (params['id'] == -1)
          this.projectsService.create(new ProjectCreateDto(
            this.projectDto.name, this.projectDto.link,
            this.imgFile, this.projectDto.tags
          ))
          .subscribe(_ => this.navigatToReturnUrl());
        else
          this.projectsService.update(params['id'], new ProjectCreateDto(
            this.projectDto.name, this.projectDto.link,
            this.imgFile, this.projectDto.tags
          ))
          .subscribe(_ => this.navigatToReturnUrl());
      });
    }
  }
}
