import {Directive, ElementRef, Input, OnInit, Renderer2} from '@angular/core';

@Directive({
  selector: '[taskState]'
})
export class StateDirective implements OnInit{
  @Input() state?: number;

  constructor(private el: ElementRef, private r: Renderer2) {}

  ngOnInit(): void {
    switch (this.state){
      case 0:
        this.r.setStyle(this.el.nativeElement, 'color', 'AQUAMARINE')
        break
      case 1:
        this.r.setStyle(this.el.nativeElement, 'color', 'MEDIUMORCHID')
        break
      case 2:
        this.r.setStyle(this.el.nativeElement, 'color', 'CHARTREUSE')
        break
      case 3:
        this.r.setStyle(this.el.nativeElement, 'color', 'RED')
        break
    }
  }



}
