import { Component, Input, SimpleChanges } from '@angular/core';
import { ButtonComponent } from '../../buttons/button/button.component';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { ApiService } from '../../../Services/api/ApiService.service';

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrl: './dialog-delete.component.css'
})
export class DialogDeleteComponent {
  constructor(public dialogService: DialogService){

  }
  back(){
    this.dialogService.RemoveDeleteMethod();
  }
}
