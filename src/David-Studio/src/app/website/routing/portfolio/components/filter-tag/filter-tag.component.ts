import { Component, Input } from '@angular/core';
import { Tag } from '../../models';
import { AppColors } from 'src/app/website/consts';

@Component({
  selector: 'portfolio-filter-tag',
  templateUrl: 'filter-tag.component.html',
  styleUrls: [ 'filter-tag.component.css' ]
})
export class FilterTagComponent {
  @Input() filter_tags: Array<Tag> = [
    new Tag('C#', AppColors.cs),
    new Tag('ASP.NET Core', AppColors.aspnet),
    new Tag('Angular', AppColors.angular),
    new Tag('Arduino', AppColors.arduino),
    new Tag('WPF', AppColors.wpf)
  ];
}