import { Component, Input } from '@angular/core';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { FormType } from '../../Interfaces/tools/Form/FormType';
import { ApiService } from '../../Services/api/ApiService.service';
import { FormService } from '../../Services/form/form.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation-mobile',
  templateUrl: './navigation-mobile.component.html',
  styleUrl: './navigation-mobile.component.css'
})
export class NavigationMobileComponent {
  openedSideBar:boolean = false;

  @Input()
  apiService!: ApiService<any,any>;

  @Input()
  form! : any ;
  @Input()
  inputsFormFields! :FormField[];

  @Input()
  formService!: FormService;
  
  @Input()
  histories! : string ;

  constructor(private router: Router){

  }
  ActiveForm(){
    if(!this.apiService) return;
    this.form.reset();
    this.formService.SetInputsField(this.inputsFormFields);
    this.formService.SetFormAndActive(FormType.ADD, this.form, this.apiService);
  }
  Navigate(){
    this.router.navigate([this.histories]);
  }
  
  openSideBar(value:boolean){
    this.openedSideBar = value;
  }
}
