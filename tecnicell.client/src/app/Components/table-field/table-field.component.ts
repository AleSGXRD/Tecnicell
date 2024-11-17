import { Component, Input } from '@angular/core';
import { TableField } from '../../Interfaces/tools/Table/TableField';

@Component({
  selector: 'app-table-field',
  templateUrl: './table-field.component.html',
  styleUrl: './table-field.component.css'
})
export class TableFieldComponent {
  @Input()
  public value! : any;
  @Input()
  public property! : TableField;
  @Input()
  public space! : string;
}
