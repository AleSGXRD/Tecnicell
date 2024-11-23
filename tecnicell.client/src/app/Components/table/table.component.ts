import { Component, Input } from '@angular/core';
import { HeaderField, SpacesField } from '../../Interfaces/tools/Table/HeaderField';
import { ButtonComponent } from "../buttons/button/button.component";
import { FormService } from '../../Services/form/form.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormType } from '../../Interfaces/tools/Form/FormType';
import { DialogService } from '../../Services/dialog/dialog.service';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { TableField, TableFieldType } from '../../Interfaces/tools/Table/TableField';
import { TableProperties } from '../../Interfaces/tools/Table/TableProperties';
import { ApiService } from '../../Services/api/ApiService.service';
import { ActionsTable } from '../../Interfaces/tools/Actions';
import { FilterTableService } from '../../Services/Filter/filter-table.service';
import { FilterField } from '../../Interfaces/tools/Filters/Filters';
import { NotificationSystemService } from '../../Services/notification-system.service';
import { BehaviorSubject } from 'rxjs';
import { toZonedTime} from 'date-fns-tz'

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {


  actionsHeader : HeaderField ={
    name: 'Actions',
    space : SpacesField.normal
  }
  //Table 
  @Input()
  table!:TableProperties;

  valuesDefault : any[] = [];

  //Form Edit
  @Input()
  inputsFormFields! :FormField[];
  @Input()
  form! : any;

  //Filters
  @Input()
  filterTableService! : FilterTableService;
  filterOptions! : FilterField[];

  @Input()
  formService! : FormService;
  @Input()
  dialogService! : DialogService;
  @Input()
  apiService! : ApiService<any,any>;

  @Input()
  actions : ActionsTable = ActionsTable.NONE;
  
  maxElements :number = 4;

  constructor(private formBuilder: FormBuilder,
    private notifcationService : NotificationSystemService
  ){}
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.valuesDefault = this.table.values;
    this.filterTableService?.filters.subscribe(
      res => {
        this.filterOptions = res;
        this.filterData();
      }
    );
  }

  currentSheet : number =0;

  convertToLocalTime(date : any):Date |undefined{
    if(date == undefined) return undefined;
    const time = new Date(date);
    const timeZoneOffset = -5; // Zona horaria de La Habana (UTC-5)
    const localDate = new Date(time.getTime() + (timeZoneOffset * 60 * 60 * 1000));
    return localDate;
  }

  sheets(){
    return Math.ceil(this.table.values.length / this.maxElements)-1;
  }
  passNextSheet(){
    this.currentSheet += 1;
    if(this.currentSheet > this.sheets()){
      this.currentSheet = 0;
    }
  }
  passPrevSheet(){
    this.currentSheet -= 1;
    if(this.currentSheet <0){
      this.currentSheet = this.sheets();
    }
  }
  filterData(){
    if(this.filterOptions != null){
      let tableValues = this.valuesDefault;
      for(let filter of this.filterOptions){
        if(filter.value == undefined) continue;

        if(filter.type == "date"){
          if(Number.isNaN(Date.parse(filter.value.start)) || 
              Number.isNaN(Date.parse(filter.value.end))) continue;
            tableValues = tableValues.filter(
              value =>{
                let show = false;
                if(value[filter.propertyName] != undefined) {
                  show = (Date.parse(value[filter.propertyName]) >= Date.parse(filter.value.start) &&
                  Date.parse(value[filter.propertyName]) <= Date.parse(filter.value.end))
                }
                if(show == true) return show;

                if(filter.otherProperties != undefined){
                  
                  filter.otherProperties.forEach(property =>
                    {
                      if(value[property.propertyName] == undefined || show == true) return;
                      if(property.subPropertyName != undefined && value[property.propertyName][property.subPropertyName] != null){
                        show = Date.parse(value[property.propertyName][property.subPropertyName]) >= Date.parse(filter.value.start) &&
                        Date.parse(value[property.propertyName][property.subPropertyName]) <= Date.parse(filter.value.end)
                      }
                      else
                        show = Date.parse(value[property.propertyName]) >= Date.parse(filter.value.start) &&
                        Date.parse(value[property.propertyName]) <= Date.parse(filter.value.end)
                    }
                  )
                }

                return show
              }
            )
        }
        if(filter.type == "select"){
          const array = Array(...filter.value);
          if(array.length > 0){
            tableValues = tableValues.filter(
              value =>{
                if(value[filter.propertyName] == undefined) return false;

                const exists = array.find(options => options.value == value[filter.propertyName])
                return exists 
              }
            )
          }
        }
        if(filter.type == "text"){
          tableValues = tableValues.filter(
            value => {
                if(value[filter.propertyName] == undefined) return false;
                console.log(value[filter.propertyName])
                return value[filter.propertyName].toLowerCase().includes(filter.value.toLowerCase())
          })
        }
      }
      this.table.values = tableValues;
      this.filterTableService.values.next({
        filter:this.filterOptions[0],
        values: this.table.values
      })
    }
    else{
      this.table.values = this.valuesDefault;
      console.log(this.table.values)
    }
  }

  editData(index : number){
    this.table.tableFields.forEach(
      element =>{
        if(element.type != TableFieldType.Link)
          if(element.subPropertyName == undefined)
            this.form.get(element.propertyName)?.setValue(this.table.values[index][element.propertyName])
          else
          {
            if(this.table.values[index][element.propertyName] != null)
              this.form.get(element.subPropertyName)?.setValue(this.table.values[index][element.propertyName][element.subPropertyName])
          }
      });
    this.formService.idEditting = this.table.values[index];

    this.formService.SetInputsField(this.inputsFormFields);
    this.formService.SetFormAndActive(FormType.EDIT, this.form, this.apiService);
  }
  deleteData(index:number){
    this.dialogService.SetDeleteMethod(()=>{
      this.apiService.delete(this.table.values[index]).subscribe(
        res => {
          this.notifcationService.showNotifcation("Se ha eliminado un elemento", 1)
        },
        err => 
          console.log(err)
      );
    });
  }
  resolveStyles(property:TableField,$index:number){
    if(property.styles == undefined)return ' ';
    let styles = property.styles.map((value, index, array) => {
      if(value.condition != undefined ){
        const field = this.table.values[$index][value.condition.formControlName];
        if(field == value.condition.value)
          return value.style
      }
      return ''
    })
    return styles.join(' ')
  }

}
