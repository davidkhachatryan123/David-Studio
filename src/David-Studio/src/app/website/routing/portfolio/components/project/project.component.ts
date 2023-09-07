import { Component, Input } from '@angular/core';
import { ProjectReadDto } from 'src/app/website/dto';

@Component({
  selector: 'portfolio-project',
  templateUrl: 'project.component.html',
  styleUrls: [ 'project.component.css' ]
})
export class PortfolioProjectComponent {
  @Input() projectModel: ProjectReadDto = new ProjectReadDto(-1, '', '', '', []);
}