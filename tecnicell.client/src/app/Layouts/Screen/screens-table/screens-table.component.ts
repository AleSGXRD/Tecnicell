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
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { ScreenApiService } from '../../../Services/api/Screen/screen-api.service';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { StateStyleCustom } from '../../../Logic/TableFieldCustoms';
import { SupplierApiService } from '../../../Services/api/Extras/supplier-api.service';

@Component({
  selector: 'app-screens-table',
  templateUrl: './screens-table.component.html',
  styleUrl: './screens-table.component.css'
})
export class ScreensTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Pantallas"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.normal
      },
      {
        name:'Marca',
        space: SpacesField.small
      },
      {
        name:'Modelo',
        space: SpacesField.small
      },
      {
        name:'Precio de venta',
        space: SpacesField.small
      },
      {
        name:'Días de garantía',
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
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "code",
        show:true,
        link : {
          url:'screen/',
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
        propertyName : "warranty",
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
        styles: StateStyleCustom
      }
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    name: ['', Validators.required],
    brand:       ['', [Validators.required]],
    salePrice:    [undefined, []],
    actionHistory: ['', [Validators.required]],
    description: ['', []],
    quantity: [undefined, [Validators.required]],
    warrantyScreen :[undefined, [Validators.required]],
    sale: [false, []],
    currencyCode:  [undefined, []],
    supplierCode: [undefined, []],
    cost: [undefined,[]],
    warranty:  [null,[]],
    branchCode:[null,[]]
  })
  inputsFormFields :FormField[]= [
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
      name: "Modelo de la Pantalla.",
      placeholder : "Modelo de la Pantalla...",
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
      type : "number",
      formControlName:"warrantyScreen",
      name: "Cantidad de días de garantía.",
      placeholder : "0",
      fieldRequired : true,
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
      errors : [],
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
      name: 'Tipo de Pantalla.',
      type: FilterType.SELECT,
      propertyName : 'type',
    },
    {
      name:'Modelo',
      type:FilterType.TEXT,
      save:true,
      propertyName: 'name'
    }
  ]

  currencyValues! : FormFieldOption[] ;
  actionsValues! : FormFieldOption[];
  branchesValues! : FormFieldOption[];
  brandValues! : FormFieldOption[];
  supplierValues! : FormFieldOption[]

  actionsTable: ActionsTable = ActionsTable.DELETE;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: ScreenApiService,
    private formBuilder : FormBuilder,

    private currencyApi : CurrencyApiService,
    private actionsApi : ActionHistoryApiService,
    private branchesApi : BranchApiService,
    private brandsApi : BrandApiService,
    private supplierApi : SupplierApiService
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.apiService.select().subscribe(res => {this.table.values = res;});
    
    
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
