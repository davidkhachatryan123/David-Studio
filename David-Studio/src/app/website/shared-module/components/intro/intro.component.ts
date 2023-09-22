import { Component, AfterContentInit, Input } from '@angular/core';
import { ViewportScroller } from "@angular/common";
import { Hexagon } from './hexagon/hexagon';

@Component({
  selector: 'app-intro',
  templateUrl: 'intro.component.html',
  styleUrls: [ 'intro.component.css' ]
})
export class IntroComponent implements AfterContentInit {
  @Input() title = '';
  @Input() subtitle = '';

  private hexagon: Hexagon;

  constructor(
    private scroller: ViewportScroller,
  ) {
    this.hexagon = new Hexagon();
  }

  ngAfterContentInit() {
    this.hexagon.init();

    addEventListener("resize", () => this.onResized());
  }

  onResized() {
    this.hexagon.resize(document.documentElement.clientWidth, document.documentElement.clientHeight);
  }

  scrollToPage() {
    this.scroller.scrollToAnchor("page");
  }
}