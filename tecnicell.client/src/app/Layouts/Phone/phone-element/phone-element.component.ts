import { Component } from '@angular/core';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { FormBuilder, Validators } from '@angular/forms';
import { FormType } from '../../../Interfaces/tools/Form/FormType';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { ActivatedRoute } from '@angular/router';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { PhoneResponse } from '../../../Interfaces/business/ApiResponses/PhoneResponse';
import { PhoneApiService } from '../../../Services/api/Phone/phone-api.service';
import { PhoneHistoryApiService } from '../../../Services/api/Phone/phone-history-api.service';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { ActionStyleCustom, MoneyStyleCustom } from '../../../Logic/TableFieldCustoms';
import { SupplierApiService } from '../../../Services/api/Extras/supplier-api.service';
import { ActionsType, filterActions } from '../../../Logic/Actions';


@Component({
  selector: 'app-phone-element',
  templateUrl: './phone-element.component.html',
  styleUrl: './phone-element.component.css'
})
export class PhoneElementComponent {
  id : any;
  loaded : boolean[] = [false,false,false,false];

  direction : string[] = ["Main", "Teléfonos"];
  value!: PhoneResponse;
  tableHistories:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Fecha',
        space: SpacesField.normal
      },
      {
        name:'Acción',
        space: SpacesField.small
      },
      {
        name: "Usuario",
        space: SpacesField.small
      },
      {
        name: 'Proveedor',
        space : SpacesField.small
      },
      {
        name:'Sucursal',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.normal
      },
      {
        name:'Moneda',
        space: SpacesField.small
      },
      {
        name:'Costo Venta',
        space: SpacesField.small
      },
      {
        name:'Garantía',
        space: SpacesField.small
      },
    ],
    tableFields :[
      {
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionHistory",
        show:true,
        styles: ActionStyleCustom
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Select,
        propertyName : "toBranch",
        show:true,
      },
      {
        type: TableFieldType.Select,
        propertyName: "supplierCode",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName:"currencyCode",
        show:true,
      },
      {
        type : TableFieldType.Revenue,
        propertyName : "saleCodeNavigation",
        subPropertyName:"cost",
        show:true,
        styles: MoneyStyleCustom
      },
      {
        type : TableFieldType.Date,
        propertyName : "saleCodeNavigation",
        subPropertyName: "warranty",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName:"saleCode",
        show:false,
      },
      {
        type: TableFieldType.Property,
        propertyName : 'userCode',
        show: false
      }
    ], 
  };

  
  //Form
  formHistories! : any ;
  inputsFormFields :FormField[]= [
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
      formControlName:"toBranch",
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
      type : "select", 
      formControlName:"supplierCode",
      name: "Proveedor",
      placeholder : "Proveedor...",
      fieldRequired : false,
      options:[],
      condition:{
        formControlName: 'actionHistory',
        value : ["Entrada"],
      },
      errors : [],
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
  historiesFilterService : FilterTableService = new FilterTableService(); 
  filtersOptions : FilterField[]=[
    {
      name: 'Rango de fecha',
      type: FilterType.DATE,
      propertyName : 'date',
      value: {start:Date, end:Date}
    },
    {
      name: 'Acciones',
      type: FilterType.SELECT,
      propertyName : 'actionHistory',
    },
    {
      name: 'Proveedores',
      type: FilterType.SELECT,
      propertyName : 'supplierCode',
    },
    {
      name:'Descripción',
      type:FilterType.TEXT,
      propertyName: 'description'
    }
  ]

  actionsTable : ActionsTable = ActionsTable.BOTH;

  currencyValues! : FormFieldOption[] ;
  actionsValues! : FormFieldOption[];
  branchesValues! : FormFieldOption[];
  brandsValues! : FormFieldOption[];
  supplierValues! : FormFieldOption[]

  constructor(private route:ActivatedRoute,
    public dialogService : DialogService,
    public formService: FormService,
    public formBuilder : FormBuilder,

    public apiService: PhoneApiService,
    public historyService : PhoneHistoryApiService,

    private currencyApi : CurrencyApiService,
    private actionsApi : ActionHistoryApiService,
    private branchesApi : BranchApiService,
    private brandsApi : BrandApiService,
    private supplierApi: SupplierApiService
  ){
    
  }
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    await this.route.params.subscribe(
      params =>{
        this.id = params['id'];
        this.direction.push(this.id);
        this.apiService.get(this.id).subscribe(
          res => {
            this.value = res;
            this.tableHistories.values = this.value.histories;
            
            this.formHistories = this.formBuilder.nonNullable.group({
              imei: [this.value.view.code,[]],
              salePrice: [this.value.view.salePrice, []],
              userCode:[undefined, []],
              date:[new Date(),[]],
              actionHistory: ['', [Validators.required]],
              description: ['', []],
              quantity: [undefined, [Validators.required]],
              sale: [false, []],
              saleCode: [null, []],
              currencyCode:  [undefined, []],
              supplierCode: [undefined,[]],
              cost: [undefined,[]],
              warranty:  [null,[]],
              toBranch:[undefined,[]]
            })
            this.loaded[0] = true;
          },
          err => console.log(err) 
        )
      }
    );
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
      const tableCodeNavigation = this.tableHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "supplierCode"
          }
          return element.propertyName == "supplierCode";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(supplier=>{
          return {
            key : supplier.supplierCode,
            value : supplier.name
          } 
        })

      const filterField = this.filtersOptions.find(filter => filter.propertyName == "supplierCode")
      if(filterField != null)
        filterField.options = this.supplierValues.map(action =>{
            return {
              name : action.name,
              value : action.value
            }
          }
        )
    })
    
    await this.currencyApi.select().subscribe(res => {
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
      const tableCodeNavigation = this.tableHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "currencyCode"
          }
          return false;
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(currency=>{
          return {
            key : currency.currencyCode,
            value : currency.currencyName
          }
        })
        
        this.loaded[1] = true;
    })
    await this.actionsApi.select().subscribe(res => {
        this.actionsValues = filterActions(res,ActionsType.PHONE).map((action:any)  => 
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
      const filterField = this.filtersOptions.find(filter => filter.propertyName == "actionHistory")
      if(filterField != null)
        filterField.options = this.actionsValues.map(action =>{
            return {
              name : action.name,
              value : action.value
            }
          }
        )
      
      this.loaded[2] = true;
    });
    await this.branchesApi.select().subscribe(res => {
      this.branchesValues = res.map(branch => {
        const field : FormFieldOption = {
          value : branch.branchCode,
          name : branch.name
        }
        return field;
      })
      const field = this.inputsFormFields.find(element => element.formControlName == "toBranch");
      if(field){
        field.options = this.branchesValues;
      }

      const tableCodeNavigation = this.tableHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "toBranch"
          }
          return element.propertyName == "toBranch";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(branch=>{
          return {
            key : branch.branchCode,
            value : branch.name
          }
        })
      this.loaded[3] = true;
    }
    );
    this.brandsApi.select().subscribe(res => {
      this.brandsValues = res.map(types => {
        const field : FormFieldOption = {
          value : types.name,
          name : types.name
        }
        return field;
      })

    })
  }

  getName(){
    return this.value.view.type;
  }
  getIdentifier(){
    return this.value.view.code;
  }
  getType(){
    return this.value.view.type;
  }
  
  editData(){
    const name = this.value.view.name.replace(this.value.view.type.toUpperCase() + " ", '');
    const form  = this.formBuilder.group({
      imei: [this.value.view.code, [Validators.required, Validators.maxLength(16), Validators.minLength(16)]],
      brand: [this.value.view.type,[Validators.required]],
      name : [name, []],
      salePrice: [this.value.view.salePrice,[Validators.required]],
    })
    const inputs :FormField[]= [
      {
        type : "textlimited",
        formControlName:"imei",
        name: "IMEI.",
        placeholder : "IMEI...",
        fieldRequired : true,
        errors : [
          {
            type: 'maxLength',
            message : 'Debe ser de 16 caracteres',
          },
          {
            type: 'minLength',
            message : 'Debe ser de 16 caracteres'
          }
        ],
        limit:16
      },
      {
        type : "select", // deberia ser fecha
        formControlName:"brand",
        name: "Marca del Teléfono.",
        placeholder : "Marca del Teléfono...",
        fieldRequired : true,
        options : this.brandsValues
      },
      {
        type : "text",
        formControlName:"name",
        name: "Modelo del teléfono.",
        placeholder : "Modelo del teléfono...",
        fieldRequired : true,
      },
      {
        type : "price",
        formControlName:"salePrice",
        name: "Precio de venta",
        placeholder : "0",
        fieldRequired : false,
      },
    ]
    this.formService.idEditting = this.value.view;

    this.formService.SetInputsField(inputs);
    this.formService.SetFormAndActive(FormType.EDIT, form, this.apiService);
  }
  deleteData(){
    this.dialogService.SetDeleteMethod(()=>{
      this.apiService.delete(this.value.view).subscribe(res => res, err => err);
    });
  }
}
