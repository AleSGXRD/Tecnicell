import { Component } from '@angular/core';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { FormBuilder, Validators } from '@angular/forms';
import { FormField } from '../../../Interfaces/tools/Form/FormField';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { AccessoryTypeApiService } from '../../../Services/api/Accessory/accessory-type-api.service';

@Component({
  selector: 'app-accessory-types-table',
  templateUrl: './accessory-types-table.component.html',
  styleUrl: './accessory-types-table.component.css'
})
export class AccessoryTypesTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Tipos de Accesorios"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.small
      },
      {
        name:'Nombre',
        space: SpacesField.small
      }
    ],
    tableFields :[
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "accessoryTypeCode",
      },
      {
        type : TableFieldType.Property,
        propertyName : "name",
        show:true,
      }
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    name: ['', Validators.required],
  })
  inputsFormFields :FormField[]= [
    {
      type : "text",
      formControlName:"name",
      name: "Nombre del tipo de Accesorio.",
      placeholder : "Nombre...",
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

  actionsTable: ActionsTable = ActionsTable.BOTH;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: AccessoryTypeApiService,
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
