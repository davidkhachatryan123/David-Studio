import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[action-bar-item]'
})
export class ActionBarButtonDirective {
  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2) {

    this.renderer.setStyle(this.elementRef.nativeElement, "margin-right", "10px");
  }
}