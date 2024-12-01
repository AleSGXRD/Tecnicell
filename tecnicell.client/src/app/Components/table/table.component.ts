import { Component, EventEmitter, Input } from '@angular/core';
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
import { AuthService } from '../../Services/api/Authorization/auth.service';
import checkLevel, { AccessLevel } from '../../Logic/AccessLevel';
import { MultipleDeleteService } from '../../Services/multiple-delete.service';

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
  
  maxElements :number = 10;

  access!:AccessLevel;

  constructor(private formBuilder: FormBuilder,
    private notifcationService : NotificationSystemService,
    private authService : AuthService,
    private multipleDelete : MultipleDeleteService
  ){
  }
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.authService.myUser.subscribe(res=>{
      this.access = checkLevel(res.role);
      
      this.multipleDelete.canUseMultipleDelete.next(
        (this.actions ==  ActionsTable.DELETE || this.actions ==  ActionsTable.BOTH )||
        ((this.actions ==  ActionsTable.DELETE_ADMIN || this.actions ==  ActionsTable.BOTH_ADMIN) &&this.access == 2)
      );
    })
    
    this.valuesDefault = this.table.values;
    this.filterTableService?.filters.subscribe(
      res => {
        this.filterOptions = res;
        this.filterData();
      }
    );
    this.multipleDelete.deleteMultiples.subscribe(res => {
      this.deleteAll();
    })

  }

  currentSheet : number =0;

  convertToLocalTime(date : any):Date |undefined{
    if(date == undefined) return undefined;
    const time = new Date(date);
    const timeZoneOffset = -5; // Zona horaria de La Habana (UTC-5)
    const localDate = new Date(time.getTime() + (timeZoneOffset * 60 * 60 * 1000));
    return localDate;
  }
  onInputLimit(event:Event){
    const input = event.target as HTMLInputElement;
    if (parseInt(input.value) > (this.sheets() + 1)) {
      input.value = "1"; // Limitar a 2 caracteres
    }
    if(parseInt(input.value) < 1){
      input.value = (this.sheets()+1).toString();
    }
    this.currentSheet = parseInt(input.value)-1;
  }

  onCheckChange(checked:boolean, index:number){
    this.table.values[index].check = checked;

    if(checked == true){
      if(this.multipleDelete.canDelete.value == false)
        this.multipleDelete.canDelete.next(true);
    }

    if(checked == false){
      let desactive = true;
      for(const element of this.table.values){
        if(element.check != undefined){
            if(element.check == true){
              desactive =false;
            }
        }
      }
      if(desactive){
        this.multipleDelete.canDelete.next(false);
      }
    }
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

  async deleteOne(element:any): Promise<boolean>{
    return new Promise<boolean>((resolve,reject)=>{
      this.apiService.delete(element).subscribe(
        res =>{
          resolve(true);
        },
        err => {
          console.log(err);
          resolve(false);
        }
      )
    })
  }
  deleteAll(){
    this.dialogService.SetDeleteMethod(async ()=>{
      let error = false;
      const listElements = this.table.values.filter((element,index)=>{
        return element.check == true
      })

      for (const element of listElements){
        const success = await this.deleteOne(element);
        if(success == false){
          error = true;
        }
      }

      if(error){
        this.notifcationService.showNotifcation("Parece que ocurrio un error al intentar eliminar un elemento.", 1);
      }
      else{
        this.notifcationService.showNotifcation("Se han eliminado todos los elementos",0)
      }
      this.multipleDelete.canDelete.next(false);
    })
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
