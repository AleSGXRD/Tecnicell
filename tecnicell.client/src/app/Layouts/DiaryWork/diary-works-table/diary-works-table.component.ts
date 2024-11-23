import { Component } from '@angular/core';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { FormBuilder, Validators } from '@angular/forms';
import { PhoneRepairApiService } from '../../../Services/api/PhoneRepair/phone-repair-api.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { FormService } from '../../../Services/form/form.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { CurrentStateStyleCustom, MoneyStyleCustom } from '../../../Logic/TableFieldCustoms';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { DiaryWorkApiService } from '../../../Services/api/DiaryWork/diary-work-api.service';
import { WorkTypeApiService } from '../../../Services/api/Extras/work-type-api.service';
import { CurrencyGains } from '../../../Interfaces/tools/CurrencyGains';
import { calculeGains } from '../../../Logic/CalculeGains';
import { getFirstDayOfWeek, getLastDayOfWeek } from '../../../Logic/DaysController';

@Component({
  selector: 'app-diary-works-table',
  templateUrl: './diary-works-table.component.html',
  styleUrl: './diary-works-table.component.css'
})
export class DiaryWorksTableComponent {

  //Direction Page
  direction : string[]=['Main', "Trabajos diarios"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Tipo de trabajo',
        space: SpacesField.normal
      },
      {
        name:'Usuario',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
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
      {
        name:'Fecha',
        space: SpacesField.small
      },
    ],
    tableFields :[
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "workType",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "description",
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
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCode",
        show:false,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName:"saleCode",
        show:false,
      },
      {
        type : TableFieldType.Property,
        propertyName : "userCode",
        show:false,
      },
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    date: [new Date(), [Validators.required]],
    workType:       [undefined,[Validators.required]],
    userCode:     [undefined,[]],
    saleCode:     [undefined,[]],
    description: [undefined, []],
    sale: [false, []],
    currencyCode:  [undefined, []],
    cost: [undefined,[]],
    warranty:  [null,[]],
  })
  inputsFormFields :FormField[]= [
    {
      type : "select",
      formControlName:"workType",
      name: "Tipo de Trabajo.",
      placeholder : "Tipo de Trabajo...",
      fieldRequired : true,
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
      name: "Método de Pago",
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
      name: 'Rango de fecha',
      type: FilterType.DATE,
      propertyName : 'date',
      value: {start:Date, end:Date}
    },
    {
      name:'Tipo de trabajo',
      type:FilterType.SELECT,
      propertyName: 'workType'
    }
  ]

  currencyValues! : FormFieldOption[] ;
  currencyGains! : CurrencyGains[];

  workTypesValues! : FormFieldOption[];
  branchesValues! : FormFieldOption[];
  brandValues! : FormFieldOption[];

  actionsTable: ActionsTable = ActionsTable.BOTH;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: DiaryWorkApiService,
    private formBuilder : FormBuilder,

    private currencyApi : CurrencyApiService,
    private brandsApi : BrandApiService,
    private workTypesApi :WorkTypeApiService
  ){
    this.filtersOptions[0].value = {
      start : getFirstDayOfWeek(),
      end : getLastDayOfWeek()
    }
    this.FilterService.emit(this.filtersOptions);
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    

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
      
      this.currencyGains = res.filter(currency => currency.currencyCode != 'none').map(currency => {
        const field : CurrencyGains = {
          value : currency.currencyCode,
          name : currency.currencyName,
          money : 0
        }
        return field;
      })
      const tableCodeNavigation = this.table.tableFields.filter(
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
    })
    this.workTypesApi.select().subscribe(res => {
        this.workTypesValues = res.map(action => 
            {
              const field : FormFieldOption = {
                value : action.name,
                name : action.name
              }
              return field;
            }
          )
      const field = this.inputsFormFields.find(element => element.formControlName == "workType");
      if(field){
        field.options = this.workTypesValues;
      }
      const filterField = this.filtersOptions.find(filter => filter.propertyName == "workType")
      if(filterField != null)
        filterField.options = this.workTypesValues.map(type =>{
            return {
              name : type.name,
              value : type.value
            }
          }
        )
    });
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
    
    this.apiService.select().subscribe(res => {
      this.table.values = res;
      this.currencyGains = calculeGains(this.currencyGains, this.table.values);
    });

    this.FilterService.values.subscribe(res => {
      this.currencyGains = calculeGains(this.currencyGains, this.table.values);
    })
    this.formService.apiService = this.apiService;
  }
}
