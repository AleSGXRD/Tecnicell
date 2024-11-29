import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormService } from '../../../Services/form/form.service';
import { FormBuilder, FormControlName, Validators } from '@angular/forms';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { ApiService } from '../../../Services/api/ApiService.service';
import { HeaderField, SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { TableField, TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { AccessoryApiRequestService } from '../../../Services/api/Accessory/accessory-api-request.service';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { Currency } from '../../../Interfaces/business/Models/Currency';
import { ActionHistory } from '../../../Interfaces/business/Models/ActionHistory';
import { Branch } from '../../../Interfaces/business/Models/Branch';
import { AccessoryTypeApiService } from '../../../Services/api/Accessory/accessory-type-api.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FormType } from '../../../Interfaces/tools/Form/FormType';
import { UserInfo } from '../../../Interfaces/business/Models/UserAccount';
import { AuthService } from '../../../Services/api/Authorization/auth.service';
import { ActionStyleCustom, StateStyleCustom } from '../../../Logic/TableFieldCustoms';
import { SupplierApiService } from '../../../Services/api/Extras/supplier-api.service';

@Component({
  selector: 'app-accessories-table',
  templateUrl: './accessories-table.component.html',
  styleUrl: './accessories-table.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AccessoriesTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Accesorios"];

  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Tipo de Accesorio',
        space: SpacesField.small
      },
      {
        name:'Nombre',
        space: SpacesField.small
      },
      {
        name:'Precio de venta',
        space: SpacesField.small
      },
      {
        name:'Cantidad',
        space: SpacesField.small
      },
      {
        name:'Estado',
        space: SpacesField.small
      },
      {
        name:'Codigo',
        space: SpacesField.normal
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
        propertyName : "name",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "salePrice",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "quantity",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "available",
        show:true,
        styles:StateStyleCustom
      },
      {
        type : TableFieldType.Link,
        propertyName : "code",
        show:true,
        link : {
          url:'accessory/',
          idPropertyName:'code'
        }
      },
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    name: ['', Validators.required],
    accessoryType:       ['', [Validators.required]],
    salePrice:    [undefined, []],
    actionHistory: ['', [Validators.required]],
    description: ['', []],
    quantity: [undefined, [Validators.required]],
    sale: [false, []],
    currencyCode:  [undefined, []],
    supplierCode: [undefined,[]],
    cost: [0,[]],
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
      type : "select",
      formControlName:"accessoryType",
      name: "Tipo de Accesorio.",
      placeholder : "Tipo de Accesorio...",
      fieldRequired : true,
      errors : [{
        type : 'minlength',
        message: 'El campo debe ser solo de 11 caracteres'
      },{
        type : 'maxlength',
        message: 'El campo debe ser solo de 11 caracteres'
      }]
    },
    {
      type : "text",
      formControlName:"name",
      name: "Nombre de Accesorio.",
      placeholder : "Nombre...",
      fieldRequired : true,
      errors : [{
        type : 'required',
        message: 'Se necesita rellenar este campo'
      }],
    },
    {
      type : "price",
      formControlName:"salePrice",
      name: "Precio de Venta",
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
      type : "select",
      formControlName:"supplierCode",
      name: "Proveedor",
      placeholder : "Proveedor...",
      fieldRequired : false,
      options:[],
      condition:{
        formControlName: 'actionHistory',
        value : ["Entrada"],
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
      type : "number",
      formControlName:"quantity",
      name: "Cantidad de productos",
      placeholder : "0",
      fieldRequired : true,
    },
    {
      type : "collapse",
      formControlName:"sale",
      name: "Metodo de Pago",
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
      name: 'Tipo de accesorios',
      type: FilterType.SELECT,
      propertyName : 'typeCode',
    },
    {
      name:'Nombre',
      save:true,
      type:FilterType.TEXT,
      propertyName: 'name'
    }
  ]

  currencyValues! : FormFieldOption[] ;
  actionsValues! : FormFieldOption[];
  branchesValues! : FormFieldOption[];
  supplierValues! : FormFieldOption[];
  accessoryTypesValues! : FormFieldOption[];

  actionsTable: ActionsTable = ActionsTable.DELETE_ADMIN;  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: AccessoryApiRequestService,
    private formBuilder : FormBuilder,

    private currencyApi : CurrencyApiService,
    private actionsApi : ActionHistoryApiService,
    private branchesApi : BranchApiService,
    private accessoryTypesApi : AccessoryTypeApiService,
    private supplierApi : SupplierApiService
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.apiService.select().subscribe(res => {this.table.values = res; });

    this.supplierApi.select().subscribe(res => {
      res.unshift({
        supplierCode : 'none',
        name:'',
      })
      this.supplierValues = res.map(supplier => {
        const field : FormFieldOption = {
          value : supplier.supplierCode,
          name : supplier.name
        }
        return field;
      })
      const field = this.inputsFormFields.find(element => element.formControlName == "supplierCode");
      if(field){
        field.options = this.supplierValues;
      }
    })
    
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
        this.actionsValues = res.filter(action => action.name == "Entrada")
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
    this.accessoryTypesApi.select().subscribe(res => {
      this.accessoryTypesValues = res.map(types => {
        const field : FormFieldOption = {
          value : types.accessoryTypeCode,
          name : types.name
        }
        return field;
      })
      const field = this.inputsFormFields.find(element => element.formControlName == "accessoryType");
      if(field){
        field.options = this.accessoryTypesValues;
      }
      const filterField = this.filtersOptions.find(filter => filter.propertyName == "typeCode")
      if(filterField != null)
        filterField.options = this.accessoryTypesValues.map(type =>{
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
