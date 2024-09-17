import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  @Input()
  color!:string;
  @Input()
  type:string = 'button';
  @Input()
  rounded:Rounded = Rounded.NORMAL;
}
export enum Rounded{
  NORMAL = "normal",
  FULL = "full"
}
