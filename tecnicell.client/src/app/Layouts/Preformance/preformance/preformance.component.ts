import { Component } from '@angular/core';
import { calculeGains } from '../../../Logic/CalculeGains';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { CurrencyGains } from '../../../Interfaces/tools/CurrencyGains';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { AccessoryHistoryApiService } from '../../../Services/api/Accessory/accessory-history-api.service';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { ActionsType, filterActions } from '../../../Logic/Actions';
import { BatteryApiService } from '../../../Services/api/Battery/battery-api.service';
import { PhoneHistoryApiService } from '../../../Services/api/Phone/phone-history-api.service';
import { PhoneRepairHistoryApiService } from '../../../Services/api/PhoneRepair/phone-repair-history-api.service';
import { ScreenHistoryApiService } from '../../../Services/api/Screen/screen-history-api.service';
import { getFirstDayOfWeek, getLastDayOfWeek } from '../../../Logic/DaysController';
import { ActionStyleCustom, MoneyStyleCustom } from '../../../Logic/TableFieldCustoms';
import { AccessoryApiRequestService } from '../../../Services/api/Accessory/accessory-api-request.service';
import { PhoneApiService } from '../../../Services/api/Phone/phone-api.service';
import { PhoneRepairApiService } from '../../../Services/api/PhoneRepair/phone-repair-api.service';
import { ScreenApiService } from '../../../Services/api/Screen/screen-api.service';
import { BatteryHistoryApiService } from '../../../Services/api/Battery/battery-history-api.service';

@Component({
  selector: 'app-preformance',
  templateUrl: './preformance.component.html',
  styleUrl: './preformance.component.css'
})
export class PreformanceComponent {
  
  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Preformance"];

  //Table Properties
  table:any[] = [];
  tableAccessoryHistories:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Código',
        space: SpacesField.normal
      },
      {
        name:'Accessorio',
        space: SpacesField.small
      },
      {
        name:'Fecha',
        space: SpacesField.small
      },
      {
        name: 'Usuario',
        space:SpacesField.small
      },
      {
        name:'Acción',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
      },
      {
        name:'Cantidad',
        space: SpacesField.small
      },
      {
        name:'Moneda',
        space: SpacesField.small
      },
      {
        name:'Precio',
        space: SpacesField.small
      },  
      {
        name:'Garantía',
        space: SpacesField.small
      },  
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "accessoryCode",
        show:true,
        link : {
          url:'accessory/',
          idPropertyName:'accessoryCode'
        }
      },
      {
        type : TableFieldType.Select,
        propertyName : "accessoryCode",
        show:true,
      },
      {
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionHistory",
        show:true,
        styles:ActionStyleCustom
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "quantity",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName: "currencyCode",
        show:true,
      },
      {
        type : TableFieldType.Revenue,
        propertyName : "saleCodeNavigation",
        subPropertyName: "cost",
        show:true,
        styles:MoneyStyleCustom
      },
      {
        type : TableFieldType.Date,
        propertyName : "saleCodeNavigation",
        subPropertyName: "warranty",
        show:true,
      },
    ], };
  tableBatteryHistories:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.normal
      },
      {
        name:'Batería',
        space: SpacesField.normal
      },
      {
        name:'Fecha',
        space: SpacesField.small
      },
      {
        name: 'Usuario',
        space:SpacesField.small
      },
      {
        name:'Acción',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
      },
      {
        name:'Cantidad',
        space: SpacesField.small
      },
      {
        name:'Moneda',
        space: SpacesField.small
      },
      {
        name:'Precio',
        space: SpacesField.small
      },  
      {
        name:'Garantía',
        space: SpacesField.small
      },  
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "batteryCode",
        show:true,
        link : {
          url:'battery/',
          idPropertyName:'batteryCode'
        }
      },
      {
        type : TableFieldType.Select,
        propertyName : "batteryCode",
        show:true,
      },
      {
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionHistory",
        show:true,
        styles:ActionStyleCustom
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "quantity",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName: "currencyCode",
        show:true,
      },
      {
        type : TableFieldType.Revenue,
        propertyName : "saleCodeNavigation",
        subPropertyName: "cost",
        show:true,
        styles:MoneyStyleCustom
      },
      {
        type : TableFieldType.Date,
        propertyName : "saleCodeNavigation",
        subPropertyName: "warranty",
        show:true,
      },
    ], 
  };
  tablePhoneHistories:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.normal
      },
      {
        name : 'Telefono',
        space: SpacesField.normal
      },
      {
        name:'Fecha',
        space: SpacesField.small
      },
      {
        name: 'Usuario',
        space:SpacesField.small
      },
      {
        name:'Acción',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
      },
      {
        name:'Cantidad',
        space: SpacesField.small
      },
      {
        name:'Moneda',
        space: SpacesField.small
      },
      {
        name:'Precio',
        space: SpacesField.small
      },  
      {
        name:'Garantía',
        space: SpacesField.small
      },  
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "imei",
        show:true,
        link : {
          url:'phone/',
          idPropertyName:'imei'
        }
      },
      {
        type : TableFieldType.Select,
        propertyName : "imei",
        show:true,
      },
      {
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionHistory",
        show:true,
        styles: ActionStyleCustom
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "quantity",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName: "currencyCode",
        show:true,
      },
      {
        type : TableFieldType.Revenue,
        propertyName : "saleCodeNavigation",
        subPropertyName: "cost",
        show:true,
        styles: MoneyStyleCustom
      },
      {
        type : TableFieldType.Date,
        propertyName : "saleCodeNavigation",
        subPropertyName: "warranty",
        show:true,
      },
    ], 
  };
  tablePhoneRepairHistories:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.normal
      },
      {
        name:'Telefono',
        space: SpacesField.normal
      },
      {
        name:'Fecha',
        space: SpacesField.small
      },
      {
        name: 'Usuario',
        space:SpacesField.small
      },
      {
        name:'Acción',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
      },
      {
        name:'Cantidad',
        space: SpacesField.small
      },
      {
        name:'Moneda',
        space: SpacesField.small
      },
      {
        name:'Precio',
        space: SpacesField.small
      },  
      {
        name:'Garantía',
        space: SpacesField.small
      },  
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "imei",
        show:true,
        link : {
          url:'phonerepair/',
          idPropertyName:'imei'
        }
      },
      {
        type : TableFieldType.Select,
        propertyName : "imei",
        show:true
      },
      {
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionHistory",
        show:true,
        styles: ActionStyleCustom
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "quantity",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName: "currencyCode",
        show:true,
      },
      {
        type : TableFieldType.Revenue,
        propertyName : "saleCodeNavigation",
        subPropertyName: "cost",
        show:true,
        styles: MoneyStyleCustom
      },
      {
        type : TableFieldType.Date,
        propertyName : "saleCodeNavigation",
        subPropertyName: "warranty",
        show:true,
      },
    ], 
  };
  tableScreenHistories:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.normal
      },
      {
        name:'Pantalla',
        space: SpacesField.normal
      },
      {
        name:'Fecha',
        space: SpacesField.small
      },
      {
        name: 'Usuario',
        space:SpacesField.small
      },
      {
        name:'Acción',
        space: SpacesField.small
      },
      {
        name:'Descripción',
        space: SpacesField.small
      },
      {
        name:'Cantidad',
        space: SpacesField.small
      },
      {
        name:'Moneda',
        space: SpacesField.small
      },
      {
        name:'Precio',
        space: SpacesField.small
      },  
      {
        name:'Garantía',
        space: SpacesField.small
      },  
    ],
    tableFields :[
      {
        type : TableFieldType.Link,
        propertyName : "screenCode",
        show:true,
        link : {
          url:'screen/',
          idPropertyName:'screenCode'
        }
      },
      {
        type : TableFieldType.Select,
        propertyName : "screenCode",
        show:true,
      },
      {
        type : TableFieldType.Date,
        show:true,
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        show:true,
        propertyName : "userCodeNavigation",
        subPropertyName: "name"
      },
      {
        type : TableFieldType.Property,
        propertyName : "actionHistory",
        show:true,
        styles:ActionStyleCustom
      },
      {
        type : TableFieldType.Property,
        propertyName : "description",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "quantity",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "saleCodeNavigation",
        subPropertyName: "currencyCode",
        show:true,
      },
      {
        type : TableFieldType.Revenue,
        propertyName : "saleCodeNavigation",
        subPropertyName: "cost",
        show:true,
        styles:MoneyStyleCustom
      },
      {
        type : TableFieldType.Date,
        propertyName : "saleCodeNavigation",
        subPropertyName: "warranty",
        show:true,
      },
    ], 
  };


  //Filters
  FilterService : FilterTableService = new FilterTableService(); 
  filtersOptions : FilterField[]=[
    {
      name: 'Rango de fecha',
      type: FilterType.DATE,
      propertyName : 'date',
      otherProperties : [{
        propertyName : 'saleCodeNavigation',
        subPropertyName : 'warranty'
      }],
      value: {start:Date, end:Date}
    },
    {
      name:'Identificador',
      type:FilterType.TEXT,
      propertyName: 'accessoryCode'
    },
    {
      name: 'Acciones',
      type: FilterType.SELECT,
      propertyName : 'actionHistory',
    },
    {
      name:'Descripción',
      type:FilterType.TEXT,
      propertyName: 'description'
    }
  ]

  currencyValues! : FormFieldOption[];
  currencyGains! : CurrencyGains[];

  actionsValues! : FormFieldOption[];
  branchesValues! : FormFieldOption[];
  accessoryTypesValues! : FormFieldOption[];

  actionsTable: ActionsTable = ActionsTable.NONE;
  

  constructor (
    public dialogService:DialogService,

    public accessoryHistoryService: AccessoryHistoryApiService,
    public batteryHistoryService: BatteryHistoryApiService,
    public phoneHistoryService: PhoneHistoryApiService,
    public phoneRepairHistoryService: PhoneRepairHistoryApiService,
    public screenHistoryService: ScreenHistoryApiService,

    public accessoriesApi : AccessoryApiRequestService,
    public batteriesApi : BatteryApiService,
    public phonesApi : PhoneApiService,
    public phonesRepairApi : PhoneRepairApiService,
    public screenApi : ScreenApiService,

    private currencyApi : CurrencyApiService,
    private actionsApi : ActionHistoryApiService,
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
    this.accessoriesApi.select().subscribe(res => {
      const tableCodeNavigation = this.tableAccessoryHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "accessoryCode"
          }
          return element.propertyName == "accessoryCode";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(accessory=>{
          return {
            key : accessory.code,
            value :accessory.name
          }
        })
      
    })

    this.batteriesApi.select().subscribe(res => {
      
      const tableCodeNavigation = this.tableBatteryHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "batteryCode"
          }
          return element.propertyName == "batteryCode";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(model=>{
          return {
            key : model.code,
            value : model.type + " " + model.name
          }
        })
    })

    this.phonesApi.select().subscribe(res => {
      const tableCodeNavigation = this.tablePhoneHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "imei"
          }
          return element.propertyName == "imei";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(model=>{
          return {
            key : model.code,
            value : model.type + " " + model.name
          }
        })
    })

    this.phonesRepairApi.select().subscribe(res => {
      const tableCodeNavigation = this.tablePhoneRepairHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "imei"
          }
          return element.propertyName == "imei";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(model=>{
          return {
            key : model.code,
            value : model.type + " " + model.name
          }
        })
    })

    this.screenApi.select().subscribe(res => {
      const tableCodeNavigation = this.tableScreenHistories.tableFields.filter(
        element => 
          element.type == TableFieldType.Select
      )
      const fieldTable = tableCodeNavigation.find(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "screenCode"
          }
          return element.propertyName == "screenCode";
        }
      );
      if(fieldTable)
        fieldTable.cases = res.map(model=>{
          return {
            key : model.code,
            value : model.type + " " + model.name
          }
        })
    })
    
    this.currencyApi.select().subscribe(res => {
      this.currencyValues = res.map(currency => {
        const field : FormFieldOption = {
          value : currency.currencyCode,
          name : currency.currencyName
        }
        return field;
      })
      this.currencyGains = res.map(currency => {
        const field : CurrencyGains = {
          value : currency.currencyCode,
          name : currency.currencyName,
          money : 0
        }
        return field;
      })
      const tableCodeNavigation =[
       ...this.tableAccessoryHistories.tableFields.filter(element => element.type == TableFieldType.Select),
       ...this.tableBatteryHistories.tableFields.filter(element => element.type == TableFieldType.Select),
       ...this.tablePhoneHistories.tableFields.filter(element => element.type == TableFieldType.Select),
       ...this.tablePhoneRepairHistories.tableFields.filter(element => element.type == TableFieldType.Select),
       ...this.tableScreenHistories.tableFields.filter(element => element.type == TableFieldType.Select)
      ]
      const fieldTable = tableCodeNavigation.filter(
        element => {
          if(element.subPropertyName!= undefined){
            return element.subPropertyName == "currencyCode"
          }
          return false;
        }
      );
      if(fieldTable)
        fieldTable.map(value => {
          value.cases = res.map(currency=>{
            return {
              key : currency.currencyCode,
              value : currency.currencyName
            }
          })
        }) 
        

    })
    this.actionsApi.select().subscribe(res => {
        this.actionsValues = res
           .map((action:any) => 
            {
              const field : FormFieldOption = {
                value : action.name,
                name : action.name
              }
              return field;
            }
          )
          
      const filterField = this.filtersOptions.find(filter => filter.propertyName == "actionHistory")
      if(filterField != null)
        filterField.options = this.actionsValues.map(action =>{
            return {
              name : action.name,
              value : action.value
            }
          }
        )
      
    });
    
    this.accessoryHistoryService.select().subscribe(res => {
      this.tableAccessoryHistories.values = res;
      this.batteryHistoryService.select().subscribe(res => {
        this.tableBatteryHistories.values = res;
        this.phoneHistoryService.select().subscribe(res => {
          this.tablePhoneHistories.values = res;
          this.phoneRepairHistoryService.select().subscribe(res => {
            this.tablePhoneRepairHistories.values = res;
            this.screenHistoryService.select().subscribe(res =>{
              this.tableScreenHistories.values = res;
              this.table = [...this.tableAccessoryHistories.values,
                ...this.tableBatteryHistories.values, 
                ...this.tablePhoneHistories.values,
                ...this.tablePhoneRepairHistories.values,
                ...this.tableScreenHistories.values];
              
              this.currencyGains = calculeGains(this.currencyGains, this.table);
            })
          })
        }
        )
      })
    });

    this.FilterService.values.subscribe(res => {
      this.table = [...this.tableAccessoryHistories.values,
      ...this.tableBatteryHistories.values, 
      ...this.tablePhoneHistories.values,
      ...this.tablePhoneRepairHistories.values,
      ...this.tableScreenHistories.values];
      this.currencyGains = calculeGains(this.currencyGains, this.table);
    })
  }
}
