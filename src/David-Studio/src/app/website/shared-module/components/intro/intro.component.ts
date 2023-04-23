import { Component, AfterContentInit, Input } from '@angular/core';
import { ViewportScroller } from "@angular/common";
import { Hexagon } from './hexagon/hexagon';

@Component({
  selector: 'app-intro',
  templateUrl: 'intro.component.html',
  styleUrls: [ 'intro.component.css' ]
})
export class IntroComponent implements AfterContentInit {
  @Input() title: string = "";
  @Input() subtitle: string = "";

  private hexagon: Hexagon;

  constructor(
    private scroller: ViewportScroller,
  ) {
    this.hexagon = new Hexagon();
  }

  ngAfterContentInit() {
    this.hexagon.init();

    addEventListener("resize", (event) => this.onResized(event));
  }

  onResized(event: any): void {
    this.hexagon.resize(document.documentElement.clientWidth, document.documentElement.clientHeight);
  }

  scrollToPage() {
    this.scroller.scrollToAnchor("page");
  }
}