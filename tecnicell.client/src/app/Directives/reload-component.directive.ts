import { Directive, Input, OnChanges, SimpleChanges, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[reloadComponent]'
})
export class ReloadComponentDirective implements OnChanges {

  @Input() reloadComponent !: any;

  constructor(private templateRef :TemplateRef <any>,
    private viewConteinerRef:ViewContainerRef
  ) { 
    this.viewConteinerRef.createEmbeddedView(templateRef);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['reloadComponent']){
      this.viewConteinerRef.clear();
      this.viewConteinerRef.createEmbeddedView(this.templateRef);
    }
  }

}
