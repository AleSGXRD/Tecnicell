import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-background',
  templateUrl: './background.component.html',
  styleUrl: './background.component.css'
})
export class BackgroundComponent {

  @Output() desappear = new EventEmitter<any>();
}
