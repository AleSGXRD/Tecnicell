import { Component } from '@angular/core';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FormField } from '../../../Interfaces/tools/Form/FormField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';

@Component({
  selector: 'app-currencies-table',
  templateUrl: './currencies-table.component.html',
  styleUrl: './currencies-table.component.css'
})
export class CurrenciesTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Monedas"];
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
        propertyName : "currencyCode",
      },
      {
        type : TableFieldType.Property,
        propertyName : "currencyName",
        show:true,
      }
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    currencyCode: [undefined, Validators.required],
    currencyName: ['', Validators.required],
  })
  inputsFormFields :FormField[]= [
    {
      type : "text",
      formControlName:"currencyName",
      name: "Nombre de la moneda.",
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
      propertyName: 'currencyName'
    }
  ]

  actionsTable: ActionsTable = ActionsTable.BOTH;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: CurrencyApiService,
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
