import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonComponent } from "../buttons/button/button.component";

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrl: './user-info.component.css'
})
export class UserInfoComponent {
  @Input()
  name:string = "Usuario Prueba";
  @Input()
  type:string = "";
  @Input()
  identifier:string = "49123451234";

  @Output() editData = new EventEmitter<any>();
  @Output() deleteData = new EventEmitter<any>();
  
}
