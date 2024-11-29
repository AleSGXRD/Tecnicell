import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { WorkTypeApiService } from '../../../Services/api/Extras/work-type-api.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { FormService } from '../../../Services/form/form.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FormField } from '../../../Interfaces/tools/Form/FormField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';

@Component({
  selector: 'app-work-types-table',
  templateUrl: './work-types-table.component.html',
  styleUrl: './work-types-table.component.css'
})
export class WorkTypesTableComponent {
  //Direction Page
  direction : string[]=['Main', "Tipos de Trabajos"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Tipo de Trabajo',
        space: SpacesField.normal
      }
    ],
    tableFields :[
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "name",
      }
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    name: [undefined, Validators.required],
  })
  inputsFormFields :FormField[]= [
    {
      type : "text",
      formControlName:"name",
      name: "Tipo de Trabajo.",
      placeholder : "Tipo de Trabajo...",
      fieldRequired : true,
      errors : [{
        type : 'required',
        message: 'Se necesita rellenar este campo'
      }],
    },
  ]
  //Filters
  FilterService : FilterTableService = new FilterTableService(); 
  filtersOptions : FilterField[]=[
    {
      name:'Nombre',
      type:FilterType.TEXT,
      propertyName: 'name'
    }
  ]

  actionsTable: ActionsTable = ActionsTable.DELETE_ADMIN;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: WorkTypeApiService,
    private formBuilder : FormBuilder,
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.apiService.select().subscribe(res => {this.table.values = res;});

    this.formService.apiService = this.apiService;
  }
}
