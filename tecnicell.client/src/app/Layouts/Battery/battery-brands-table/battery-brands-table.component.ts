import { Component } from '@angular/core';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { FormBuilder, Validators } from '@angular/forms';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { FormService } from '../../../Services/form/form.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';

@Component({
  selector: 'app-battery-brands-table',
  templateUrl: './battery-brands-table.component.html',
  styleUrl: './battery-brands-table.component.css'
})
export class BatteryBrandsTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Marca de Baterías"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Nombre',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
      }
    ],
    tableFields :[
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "name",
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      }
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    name: ['', Validators.required],
    description:       ['', []]
  })
  inputsFormFields :FormField[]= [
    {
      type : "text",
      formControlName:"name",
      name: "Nombre de la marca.",
      placeholder : "Nombre...",
      fieldRequired : true,
      errors : [{
        type : 'required',
        message: 'Se necesita rellenar este campo'
      }],
    },
    {
      type : "textarea",
      formControlName:"description",
      name: "Descripción de la batería",
      placeholder : "Descripción ...",
      fieldRequired : false,
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
    public apiService: BrandApiService,
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
