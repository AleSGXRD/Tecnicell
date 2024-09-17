import { Component, Input } from '@angular/core';
import { HeaderField, SpacesField } from '../../types/tools/Table/HeaderField';
import { ButtonComponent } from "../buttons/button/button.component";
import { FormService } from '../../services/form/form.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormType } from '../../types/tools/Form/FormType';
import { ApiService } from '../../services/api/ApiService.service';
import { DialogService } from '../../services/dialog/dialog.service';
import { FormField } from '../../types/tools/Form/FormField';
import { TableField, TableFieldType } from '../../types/tools/Table/TableField';
import { TableProperties } from '../../types/tools/Table/TableProperties';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {


  actionsHeader : HeaderField ={
    name: 'Actions',
    space : SpacesField.normal
  }
  //Table 
  @Input()
  table!:TableProperties;

  //Form Edit
  @Input()
  inputsFormFields! :FormField[];
  @Input()
  form! : any;


  @Input()
  formService! : FormService;
  @Input()
  dialogService! : DialogService;
  @Input()
  apiService! : ApiService<any>;

  constructor(private formBuilder: FormBuilder){

  }

  editData(index : number){
    this.table.tableFields.forEach(
      element =>{
        if(element.type == TableFieldType.Property)
          this.form.get(element.propertyName)?.setValue(this.table.values[index][element.propertyName])
      });
    this.formService.idEditting = this.table.values[index].codc;

    this.formService.SetInputsField(this.inputsFormFields);
    this.formService.SetFormAndActive(FormType.EDIT, this.form);
  }
  deleteData(index:number){
    this.dialogService.SetDeleteMethod(()=>{
      this.apiService.delete(this.table.values[index].codc).subscribe(res => res, err => err);
    });
  }

}
