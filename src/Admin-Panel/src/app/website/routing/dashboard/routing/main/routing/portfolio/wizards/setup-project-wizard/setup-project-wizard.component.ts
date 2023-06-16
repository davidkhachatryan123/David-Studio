import { Component, ViewChild, ElementRef } from '@angular/core';
import { Project, Tag } from '../../../../models';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ImageCropDialogService } from '../services/image-crop-dialog.service';
import { Observable, map, startWith } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-dashboard-main-portfolio-setup-project-wizard',
  templateUrl: 'setup-project-wizard.component.html',
  styleUrls: ['setup-project-wizard.component.css']
})
export class SetupProjectWizardComponent {
  project: Project;
  projectForm: FormGroup;

  img: any = '';

  allTags: Tag[];
  filteredTags: Observable<string[]>;

  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement>;

  constructor(private imageCropDialogService: ImageCropDialogService) {
    this.projectForm = new FormGroup({
      "name": new FormControl('', [Validators.required, Validators.maxLength(128)]),
      "link": new FormControl('', [Validators.required, Validators.maxLength(512)]),
      "image": new FormControl('', [Validators.required]),
      "tagCtrl": new FormControl('', [Validators.required])
    });

    this.allTags = [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')
    ];

    this.filteredTags = this.projectForm.controls['tagCtrl'].valueChanges.pipe(
      startWith(null),
      map((course: string | null) =>
      (course ? this._filter(course) : this.allTags.map(data => data.name).slice())),
    );

    this.project = new Project();
  }

  onFileChange(event: Event) {
    this.imageCropDialogService.show(event)
    ?.afterClosed().subscribe((result: any) => {
      if(result)
        this.img = result;
    });
  }

  removeTag(tag: Tag) {
    this.project.tags.includes(tag)
      ? this.project.tags.splice(this.project.tags.indexOf(tag), 1)
      : null;
  }

  tagSelected(event: MatAutocompleteSelectedEvent) {
    if (!this.project.tags.find(tag => tag.name == event.option.value))
      this.project.tags.push(this.allTags.find(tag => tag.name == event.option.value));

    this.tagInput.nativeElement.value = '';
    this.projectForm.controls['tagCtrl'].setValue(null);

    console.log(this.project.tags);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allTags.map(tag => tag.name)
    .filter(course => course.toLowerCase().includes(filterValue));
  }

  navigatToReturnUrl() {
    // navigate to return url
  }

  submit() {
    // save changes(console.log) and then navigate to return url
  }
}
