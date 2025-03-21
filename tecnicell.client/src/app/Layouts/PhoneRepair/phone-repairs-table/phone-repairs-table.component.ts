import { Component } from '@angular/core';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { FormBuilder, Validators } from '@angular/forms';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { PhoneApiService } from '../../../Services/api/Phone/phone-api.service';
import { PhoneRepairApiService } from '../../../Services/api/PhoneRepair/phone-repair-api.service';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { CurrentStateStyleCustom } from '../../../Logic/TableFieldCustoms';

@Component({
  selector: 'app-phone-repairs-table',
  templateUrl: './phone-repairs-table.component.html',
  styleUrl: './phone-repairs-table.component.css'
})
export class PhoneRepairsTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Teléfonos a reparar"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Modelo del teléfono',
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
    ],
    tableFields :[

      {
        type : TableFieldType.Link,
        propertyName : "name",
        show:true,
        link : {
          url:'phonerepair/',
          idPropertyName:'code'
        }
      },
      {
        type : TableFieldType.Property,
        propertyName : "currentState",
        show:true,
        styles:CurrentStateStyleCustom
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
    ], 
    linkFields : {
      url:'phonerepair/',
      idPropertyName:'code'
    }
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    imei: [undefined, [Validators.required, Validators.maxLength(15), Validators.minLength(15)]],
    brand:       ['', [Validators.required]],
    name:       ['',[Validators.required]],
    price:    [undefined, []],
    customerId : [undefined, [Validators.required, Validators.maxLength(11), Validators.minLength(11)]],
    customerName: ['', [Validators.required]],
    customerNumber: ['',[]],
    actionHistory: ['', [Validators.required]],
    description: ['', []],
    sale: [false, []],
    currencyCode:  [undefined, []],
    cost: [undefined,[]],
    warranty:  [null,[]],
    branchCode:[null,[]],
    setTime: [false,[]],
    day: [undefined,[]],
    hours : [undefined,[]],
    minutes:[undefined,[]],
    seconds:[undefined, []],
    time : [undefined, []]
  })
  inputsFormFields :FormField[]= [
    {
      type : "collapse",
      formControlName:"setTime",
      name: "Fecha",
      placeholder : "",
      fieldRequired : false,
      fields: [
        {
          type : "date",
          formControlName:"day",
          name: "Dia",
          placeholder : "Dia...",
          fieldRequired : false,
          errors : []
        },
        {
          type : "time", // deberia ser fecha
          formControlName:"time",
          name: "Hora",
          placeholder : "Garantía...",
          fieldRequired : false,
          errors : []
        },
      ]
    },
    {
      type : "textlimited",
      formControlName:"imei",
      name: "IMEI.",
      placeholder : "IMEI...",
      fieldRequired : true,
      errors : [
        {
          type: 'maxLength',
          message : 'Debe ser de 15 caracteres',
        },
        {
          type: 'minLength',
          message : 'Debe ser de 15 caracteres'
        }
      ],
      limit: 15
    },
    {
      type : "select",
      formControlName:"brand",
      name: "Marca.",
      placeholder : "Marca...",
      fieldRequired : true,
    },
    {
      type : "text",
      formControlName:"name",
      name: "Modelo del teléfono.",
      placeholder : "Modelo del teléfono...",
      fieldRequired : true,
    },
    {
      type : "text",
      formControlName:"customerName",
      name: "Nombre del cliente.",
      placeholder : "Nombre ...",
      fieldRequired : true,
    },
    {
      type : "textlimited",
      formControlName:"customerId",
      name: "CI del cliente.",
      placeholder : "CI ...",
      fieldRequired : true,
      errors :[
        {
          type: 'maxLength',
          message : 'Debe ser de 11 caracteres',
        },
        {
          type: 'minLength',
          message : 'Debe ser de 11 caracteres'
        }
      ],
      limit:11
    },
    {
      type : "telephone",
      formControlName:"customerNumber",
      name: "Número del cliente.",
      placeholder : "54667788",
      fieldRequired : false,
      limit: 8
    },
    {
      type : "price",
      formControlName:"price",
      name: "Precio de reparación.",
      placeholder : "0",
      fieldRequired : false,
    },
    {
      type : "select",
      formControlName:"actionHistory",
      name: "Acción de Historial",
      placeholder : "Acción...",
      fieldRequired : true,
      options:[]
    },
    {
      type : "textarea",
      formControlName:"description",
      name: "Descripción de la acción",
      placeholder : "Descripción ...",
      fieldRequired : false,
    },
    {
      type : "collapse",
      formControlName:"sale",
      name: "Método de pago",
      placeholder : "",
      fieldRequired : false,
      fields: [
        {
          type : "select",
          formControlName:"currencyCode",
          name: "Moneda",
          placeholder : "Moneda...",
          fieldRequired : false,
          options:[]
        },
        {
          type : "price", // deberia ser fecha
          formControlName:"cost",
          name: "Costo de compra",
          placeholder : "0",
          fieldRequired : false,
          errors : []
        },
        {
          type : "date", // deberia ser fecha
          formControlName:"warranty",
          name: "Garantía hasta",
          placeholder : "Garantía...",
          fieldRequired : false,
          errors : []
        },
      ]
    }
  ]
  //Filters
  FilterService : FilterTableService = new FilterTableService(); 
  filtersOptions : FilterField[]=[
    {
      name:'IMEI',
      type:FilterType.TEXT,
      propertyName: 'code'
    },
    {
      name:'Nombre del teléfono.',
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

  currencyValues! : FormFieldOption[] ;
  actionsValues! : FormFieldOption[];
  branchesValues! : FormFieldOption[];
  brandValues! : FormFieldOption[];

  actionsTable: ActionsTable = ActionsTable.DELETE_ADMIN;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: PhoneRepairApiService,
    private formBuilder : FormBuilder,

    private currencyApi : CurrencyApiService,
    private actionsApi : ActionHistoryApiService,
    private branchesApi : BranchApiService,
    private brandsApi : BrandApiService
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.apiService.select().subscribe(res => {this.table.values = res;});
    
    this.currencyApi.select().subscribe(res => {
      res.unshift({
        currencyCode : 'none',
        currencyName:'',
      })
      this.currencyValues = res.map(currency => {
        const field : FormFieldOption = {
          value : currency.currencyCode,
          name : currency.currencyName
        }
        return field;
      })
      const field = this.inputsFormFields.find(element => element.formControlName == "sale");
      if(field){
        const subfield = field.fields?.find(element => element.formControlName == "currencyCode");
        if(subfield){
          subfield.options = this.currencyValues;
        }
      }
    })
    this.actionsApi.select().subscribe(res => {
        this.actionsValues = res.filter(action => action.name == "Entrada" || action.name == "Transferido desde otra sucursal")
           .map(action => 
            {
              const field : FormFieldOption = {
                value : action.name,
                name : action.name
              }
              return field;
            }
          )
      const field = this.inputsFormFields.find(element => element.formControlName == "actionHistory");
      if(field){
        field.options = this.actionsValues;
      }
    });
    this.branchesApi.select().subscribe(res => {
      this.branchesValues = res.map(branch => {
        const field : FormFieldOption = {
          value : branch.branchCode,
          name : branch.name
        }
        return field;
      })
      const field = this.inputsFormFields.find(element => element.formControlName == "branchCode");
      if(field){
        field.options = this.branchesValues;
      }
    }
    );
    this.brandsApi.select().subscribe(res => {
      this.brandValues = res.map(types => {
        const field : FormFieldOption = {
          value : types.name,
          name : types.name
        }
        return field;
      })
      const field = this.inputsFormFields.find(element => element.formControlName == "brand");
      if(field){
        field.options = this.brandValues;
      }
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
    this.formService.apiService = this.apiService;
  }
}
