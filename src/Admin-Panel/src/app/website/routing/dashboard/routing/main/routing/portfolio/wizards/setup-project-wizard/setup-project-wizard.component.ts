import { Component, ViewChild, ElementRef, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Observable, map, startWith } from 'rxjs';

import { ImageCropDialogService } from '../services/image-crop-dialog.service';
import { ProjectDto } from '../../models';
import { Project, Tag } from '../../../../models';

@Component({
  selector: 'app-dashboard-main-portfolio-setup-project-wizard',
  templateUrl: 'setup-project-wizard.component.html',
  styleUrls: ['setup-project-wizard.component.css']
})
export class SetupProjectWizardComponent implements OnInit {
  projectDto: ProjectDto;
  projectForm: FormGroup;

  img: any = '';
  imgFileName = '';

  allTags: Tag[];
  filteredTags: Observable<string[]>;

  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement>;

  constructor(
    private imageCropDialogService: ImageCropDialogService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    this.projectForm = new FormGroup({
      "name": new FormControl('', [Validators.required, Validators.maxLength(128)]),
      "link": new FormControl('', [Validators.required, Validators.maxLength(512)]),
      "image": new FormControl(''),
      "tagCtrl": new FormControl('')
    });

    this.projectForm.controls['name'].valueChanges
      .subscribe(value => this.projectDto.title = value);
    this.projectForm.controls['link'].valueChanges
      .subscribe(value => this.projectDto.link = value);

    this.filteredTags = this.projectForm.controls['tagCtrl'].valueChanges.pipe(
      startWith(null),
      map((course: string | null) =>
      (course ? this._filter(course) : this.allTags.map(data => data.name).slice())),
    );
  }

  ngOnInit () {
    this.route.queryParams.subscribe(params => {
      // get it from server using "params['id']" identifier of project
      let project: Project = new Project();

      this.projectDto = new ProjectDto(
        project.id, project.title,
        project.demo_link, project.tags
      );
      this.img = project.img_uri;

      this.projectForm.controls['name'].setValue(this.projectDto.title);
      this.projectForm.controls['link'].setValue(this.projectDto.link);
    });

    // Get it all from the server
    this.allTags = [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')
    ];
  }

  onFileChange(event: Event) {
    this.imageCropDialogService.show(event)
    ?.afterClosed().subscribe((result: any) => {
      if(result)
        this.img = result;
        this.projectDto.image.append(((event.target as HTMLInputElement).files as FileList)[0].name, result);
    });
  }

  removeTag(tag: Tag) {
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

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allTags.map(tag => tag.name)
    .filter(course => course.toLowerCase().includes(filterValue));
  }

  navigatToReturnUrl() {
    this.route.queryParams.subscribe(params => {
      this.router.navigate([params['returnUrl']]);
    });
  }

  submit() {
    if(this.projectForm.valid) {
      if(this.projectDto.id === -1)
        console.log('Create new Project: ', this.projectDto);
      else
        console.log('Edit existing Project: ', this.projectDto);

      this.navigatToReturnUrl();
    }
  }
}
