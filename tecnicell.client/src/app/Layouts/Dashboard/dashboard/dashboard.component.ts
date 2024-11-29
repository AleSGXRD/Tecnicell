import { Component } from '@angular/core';
import { UsdApiService } from '../../../Services/api/Usd/usd-api.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { FormService } from '../../../Services/form/form.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { FormType } from '../../../Interfaces/tools/Form/FormType';
import { Usd } from '../../../Interfaces/business/Models/Usd';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { PhoneRepairApiService } from '../../../Services/api/PhoneRepair/phone-repair-api.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { CurrentStateStyleCustom } from '../../../Logic/TableFieldCustoms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  direction : string[]=['Main', "Dashboard"];

  usdChange! : Usd;

  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Marca',
        space: SpacesField.small
      },
      {
        name:'Modelo del teléfono',
        space: SpacesField.small
      },
      {
        name:'Precio de venta',
        space: SpacesField.small
      },
      {
        name:'Estado',
        space: SpacesField.small
      },
      {
        name:'Nombre del cliente',
        space: SpacesField.small
      },
      {
        name:'CI del cliente',
        space: SpacesField.small
      },
      {
        name:'Numero del cliente',
        space: SpacesField.small
      },
      {
        name:'Descripción sobre la ultima acción',
        space: SpacesField.normal
      },
      {
        name:'IMEI',
        space: SpacesField.small
      },
    ],
    tableFields :[
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "type",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "name",
      },
      {
        type : TableFieldType.Property,
        propertyName : "price",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "currentState",
        show:true,
        styles: CurrentStateStyleCustom
      },
      {
        type : TableFieldType.Property,
        propertyName : "customerName",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "customerId",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "customerNumber",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionDescription",
        show:true,
      },
      {
        type : TableFieldType.Link,
        propertyName : "code",
        show:true,
        link : {
          url:'phonerepair/',
          idPropertyName:'code'
        }
      },
    ], 
  };

  //Filters
  FilterService : FilterTableService = new FilterTableService(); 
  filtersOptions : FilterField[]=[
    {
      name:'IMEI',
      type:FilterType.TEXT,
      propertyName: 'code'
    },
    {
      name:'Modelo del teléfono.',
      type:FilterType.TEXT,
      propertyName: 'name'
    },
    {
      name: 'Marca de teléfono.',
      type: FilterType.SELECT,
      propertyName : 'type',
    },
    {
      name:'Nombre del cliente',
      type:FilterType.TEXT,
      propertyName: 'customerName'
    },
    {
      name:'CI del cliente',
      type:FilterType.TEXT,
      propertyName: 'customerId'
    },
  ]

  actionsTable: ActionsTable = ActionsTable.NONE;
  
  brandValues! : FormFieldOption[];

  constructor(
    public dialogService : DialogService,
    public formService: FormService,
    public apiService: UsdApiService,
    private formBuilder : FormBuilder,

    public phoneRepairApi : PhoneRepairApiService,
    private brandsApi : BrandApiService,){
    this.phoneRepairApi.select().subscribe(res => 
      {
        this.table.values = res.filter(value => {
          if(value.currentState.toLowerCase().includes("entregado"))
            return false;
          return true;
        });
      }
    );
    this.apiService.usd.subscribe(res => this.usdChange = res);
    this.apiService.getLast();
  }
  ngOnInit(){
    
    this.brandsApi.select().subscribe(res => {
      this.brandValues = res.map(types => {
        const field : FormFieldOption = {
          value : types.name,
          name : types.name
        }
        return field;
      })
      const filterField = this.filtersOptions.find(filter => filter.propertyName == "type")
      if(filterField != null)
        filterField.options = this.brandValues.map(type =>{
            return {
              name : type.name,
              value : type.value
            }
          }
        )
    })
  }

  editUsd(){
    const form  = this.formBuilder.nonNullable.group({
      value: [undefined, [Validators.required]]
    })
    const inputs :FormField[]= [
      {
        type : "number",
        formControlName:"value",
        name: "Valor del dolar.",
        placeholder : "0",
        fieldRequired : true,
        errors : [{
          type : 'required',
          message: 'Se necesita rellenar este campo'
        }],
      },
    ]

    this.formService.SetInputsField(inputs);
    this.formService.SetFormAndActive(FormType.ADD, form, this.apiService);
  }
}
