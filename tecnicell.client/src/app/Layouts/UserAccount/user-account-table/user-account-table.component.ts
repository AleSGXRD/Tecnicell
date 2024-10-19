import { Component } from '@angular/core';
import { TableProperties } from '../../../Interfaces/tools/Table/TableProperties';
import { SpacesField } from '../../../Interfaces/tools/Table/HeaderField';
import { TableFieldType, Values } from '../../../Interfaces/tools/Table/TableField';
import { FormBuilder, Validators } from '@angular/forms';
import { FormField, FormFieldOption } from '../../../Interfaces/tools/Form/FormField';
import { FilterTableService } from '../../../Services/Filter/filter-table.service';
import { FilterField, FilterType } from '../../../Interfaces/tools/Filters/Filters';
import { ActionsTable } from '../../../Interfaces/tools/Actions';
import { FormService } from '../../../Services/form/form.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { UserInfoApiService } from '../../../Services/api/UserAccount/user-info-api.service';
import { RoleApiService } from '../../../Services/api/UserAccount/role-api.service';
import { BranchApiService } from '../../../Services/api/Extras/branch-api.service';
import { UserAccountApiService } from '../../../Services/api/UserAccount/user-account-api.service';

@Component({
  selector: 'app-user-account-table',
  templateUrl: './user-account-table.component.html',
  styleUrl: './user-account-table.component.css'
})
export class UserAccountTableComponent {

  load : boolean = false;

  //Direction Page
  direction : string[]=['Main', "Usuarios"];
  //Table Properties
  table:TableProperties= {
    values : [],
    headerFields : [
      {
        name:'Codigo',
        space: SpacesField.small
      },
      {
        name:'Rol',
        space: SpacesField.small
      },
      {
        name:'Sucursal',
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
        propertyName : "userCode",
      },
      {
        type : TableFieldType.Select,
        propertyName : "role",
        show:true,
      },
      {
        type : TableFieldType.Select,
        propertyName : "branch",
        show:true,
      },
      {
        type : TableFieldType.Property,
        propertyName : "name",
        show:true,
      }
    ], 
  };

  //Form
  form : any = this.formBuilder.nonNullable.group({
    userCode: [undefined],
    name: [undefined, Validators.required],
    password: [undefined, Validators.required],
    branch: [undefined,Validators.required],
    role: [undefined, Validators.required]
  })
  inputsFormFields :FormField[]= [
    {
      type : "select",
      formControlName:"role",
      name: "Rol de usuario",
      placeholder : "Rol de usuario...",
      fieldRequired : false,
    },
    {
      type : "select",
      formControlName:"branch",
      name: "Sucursal del usuario",
      placeholder : "Sucursal del usuario...",
      fieldRequired : false,
    },
    {
      type : "text",
      formControlName:"name",
      name: "Nombre de usuario.",
      placeholder : "Nombre...",
      fieldRequired : true
    },
    {
      type : "password",
      formControlName:"password",
      name: "Contraseña de usuario",
      placeholder : "Contraseña de usuario...",
      fieldRequired : false,
    },
  ]
  //Filters
  FilterService : FilterTableService = new FilterTableService(); 
  filtersOptions : FilterField[]=[
    {
      name:'Nombre',
      type:FilterType.TEXT,
      propertyName: 'name'
    },
    {
      name:'Rol',
      type:FilterType.SELECT,
      propertyName: 'role'
    },
    {
      name:'Sucursal',
      type:FilterType.SELECT,
      propertyName: 'branch'
    }
  ]

  actionsTable: ActionsTable = ActionsTable.BOTH;
  

  constructor (public formService: FormService,
    public dialogService:DialogService,
    public apiService: UserAccountApiService,
    private formBuilder : FormBuilder,

    private branchApi : BranchApiService,
    private roleApi : RoleApiService
  ){
  }
  
  async ngOnInit() {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.apiService.select().subscribe(res => {this.table.values = res;});

    this.roleApi.select().subscribe(res =>{
      const tableRow = this.table.tableFields.find(finded => finded.propertyName == "role");
      if(tableRow)
        tableRow.cases = res.map(role => 
        {
          const option : Values ={
            key: role.roleCode,
            value: role.name
          }
          return option
        }
      )
        
      const input = this.inputsFormFields.find(finded => finded.formControlName == "role");
      if(input)
        input.options = res.map(value => {
          const option :FormFieldOption = {
            value :value.roleCode,
            name : value.name 
          } 
          return option;
        })
        
      const filterField = this.filtersOptions.find(finded => finded.propertyName == "role");
      if(filterField)
        filterField.options = res.map(value => {
          const option :FormFieldOption = {
            value :value.roleCode,
            name : value.name 
          } 
          return option;
        })
    })

    this.branchApi.select().subscribe(res => {

      const tableRow = this.table.tableFields.find(finded => finded.propertyName == "branch");
      if(tableRow)
        tableRow.cases = res.map(role => 
        {
          const option : Values ={
            key: role.branchCode,
            value: role.name
          }
          return option
        }
      )

      const input = this.inputsFormFields.find(finded => finded.formControlName == "branch");
      if(input)
        input.options = res.map(value => {
          const option :FormFieldOption = {
            value :value.branchCode,
            name : value.name 
          } 
          return option;
        })

        
      const filterField = this.filtersOptions.find(finded => finded.propertyName == "branch");
      if(filterField)
        filterField.options = res.map(value => {
          const option :FormFieldOption = {
            value :value.branchCode,
            name : value.name 
          } 
          return option;
        })
    })

    this.formService.apiService = this.apiService;
  }
}
