import { Component } from '@angular/core';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableFieldType } from '../../../Interfaces/tools/Table/TableField';
import { Validators } from '@angular/forms';
import { FormField } from '../../../Interfaces/tools/Form/FormField';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { SearchsApiService } from '../../../Services/api/Extras/searchs-api.service';

@Component({
  selector: 'app-searchs-table',
  templateUrl: './searchs-table.component.html',
  styleUrl: './searchs-table.component.css'
})
export class SearchsTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Busquedas"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Fecha',
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
        propertyName : "date",
      },
      {
        type : TableFieldType.Property,
        propertyName : "value",
        show:true,
      }
    ], 
  };

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
      name:'Nombre',
      type:FilterType.TEXT,
      propertyName: 'value'
    }
  ]

  actionsTable: ActionsTable = ActionsTable.NONE;
  

  constructor (
    public dialogService:DialogService,
    public apiService: SearchsApiService,
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.apiService.select().subscribe(res => {this.table.values = res;});
  }
}
