import { Component } from '@angular/core';
import { calculeGains } from '../../../Logic/CalculeGains';
import { FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { CurrencyGains } from '../../../Interfaces/tools/CurrencyGains';
import { CurrencyApiService } from '../../../Services/api/Extras/currency-api.service';
import { ActionHistoryApiService } from '../../../Services/api/Extras/action-history-api.service';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { BatteryHistoryApiService } from '../../../Services/api/Battery/battery-history-api.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { BrandApiService } from '../../../Services/api/Extras/battery-brand-api.service';

@Component({
  selector: 'app-battery-histories',
  templateUrl: './battery-histories.component.html',
  styleUrl: './battery-histories.component.css'
})
export class BatteryHistoriesComponent {


  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Baterías", "Historial"];

  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
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
        propertyName : "batteryCode",
        show:true,
        link : {
          url:'battery/',
          idPropertyName:'batteryCode'
        }
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
        type : TableFieldType.Property,
        propertyName : "saleCodeNavigation",
        subPropertyName: "cost",
        show:true,
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
      propertyName: 'batteryCode'
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
  brandsValues! : FormFieldOption[];

  actionsTable: ActionsTable = ActionsTable.NONE;
  

  constructor (
    public dialogService:DialogService,
    public apiService: BatteryHistoryApiService,

    private currencyApi : CurrencyApiService,
    private actionsApi : ActionHistoryApiService,
    private branchesApi : BranchApiService
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    
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
    this.actionsApi.select().subscribe(res => {
        this.actionsValues = res.filter(action => action.name != "Pieza Extraida" && action.name != "Pieza Puesta" && action.name != "Armado")
           .map(action => 
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
    this.branchesApi.select().subscribe(res => {
      this.branchesValues = res.map(branch => {
        const field : FormFieldOption = {
          value : branch.branchCode,
          name : branch.name
        }
        return field;
      })
    }
    );
    
    this.apiService.select().subscribe(res => {
      this.table.values = res;
      this.currencyGains = calculeGains(this.currencyGains, this.table.values);
    });

    this.FilterService.values.subscribe(res => {
      this.currencyGains = calculeGains(this.currencyGains, this.table.values);
    })
  }
}
