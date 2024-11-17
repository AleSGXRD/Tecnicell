import { Component } from '@angular/core';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { FormBuilder, Validators } from '@angular/forms';
import { FormField } from '../../../Interfaces/tools/Form/FormField';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { SupplierApiService } from '../../../Services/api/Extras/supplier-api.service';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';

@Component({
  selector: 'app-suppliers-table',
  templateUrl: './suppliers-table.component.html',
  styleUrl: './suppliers-table.component.css'
})
export class SuppliersTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Proveedores"];
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
        propertyName : "supplierCode",
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
    supplierCode: [undefined, Validators.required],
    name: ['', Validators.required],
  })
  inputsFormFields :FormField[]= [
    {
      type : "text",
      formControlName:"name",
      name: "Nombre del proveedor.",
      placeholder : "Nombre del proveedor...",
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
    public apiService: SupplierApiService,
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
