import { Component } from '@angular/core';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: [ 'search.component.css' ]
})
export class SearchComponent {
  isShow: boolean = false;

  text_result: Array<string> = [
    'David Studio',
    'Study Control Software',
    'Text Analyzer'
  ];

  tag_result: Array<Tag> = [
    new Tag('C#', AppColors.cs),
    new Tag('ASP.NET Core', AppColors.aspnet),
    new Tag('Angular', AppColors.angular),
    new Tag('Arduino', AppColors.arduino),
    new Tag('WPF', AppColors.wpf),
  ];
}