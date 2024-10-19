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
        name:'IMEI',
        space: SpacesField.small
      },
      {
        name:'Marca',
        space: SpacesField.small
      },
      {
        name:'Nombre del teléfono',
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
      }
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "code",
        show:true,
        link : {
          url:'phonerepair/',
          idPropertyName:'code'
        }
      },
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
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    imei: [undefined, Validators.required],
    brand:       ['', [Validators.required]],
    name:       ['',[Validators.required]],
    price:    [undefined, []],
    customerId : [undefined, [Validators.required, Validators.maxLength(11), Validators.minLength(11)]],
    customerName: ['', [Validators.required]],
    customerNumber: ['',[Validators.required]],
    actionHistory: ['', [Validators.required]],
    description: ['', []],
    sale: [false, []],
    currencyCode:  [undefined, []],
    cost: [undefined,[]],
    warranty:  [null,[]],
    branchCode:[null,[]]
  })
  inputsFormFields :FormField[]= [
    {
      type : "text",
      formControlName:"imei",
      name: "IMEI.",
      placeholder : "IMEI...",
      fieldRequired : true,
      errors : [{
        type : 'required',
        message: 'Se necesita rellenar este campo'
      }],
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
      name: "Nombre del teléfono.",
      placeholder : "Nombre del teléfono...",
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
      type : "text",
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
      ]
    },
    {
      type : "telephone",
      formControlName:"customerNumber",
      name: "Número del cliente.",
      placeholder : "54667788",
      fieldRequired : true,
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
      type : "select", // deberia ser fecha
      formControlName:"branchCode",
      name: "Sucursal",
      placeholder : "Sucursal...",
      fieldRequired : false,
      errors : [],
      condition:{
        formControlName: 'actionHistory',
        value : ["Transferido desde otra sucursal", "Transferido hacia otra sucursal"],
      }
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
      name: "Compra",
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

  actionsTable: ActionsTable = ActionsTable.DELETE;
  

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
