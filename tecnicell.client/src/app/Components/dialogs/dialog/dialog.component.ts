import { Component } from '@angular/core';
import { ButtonComponent } from "../../buttons/button/button.component";
import { DialogService } from '../../../Services/dialog/dialog.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.css'
})
export class DialogComponent {
  constructor(public dialogService: DialogService){

  }
  back(){
    this.dialogService.RemoveMethod();
  }
}
