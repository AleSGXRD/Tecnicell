import { Component, Input } from '@angular/core';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { FormType } from '../../Interfaces/tools/Form/FormType';
import { ApiService } from '../../Services/api/ApiService.service';
import { FormService } from '../../Services/form/form.service';
import { Router } from '@angular/router';
import { MultipleDeleteService } from '../../Services/multiple-delete.service';
import { Rounded } from '../buttons/button/button.component';

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

  public openedNavigation : boolean  = false;
  get cantOptions(){
    const cant = (this.form? 1 : 0)+ (this.histories? 1 : 0) + (this.canUseMultipleDelete?1:0)
    return cant;
  }


  canUseMultipleDelete :boolean = false;
  canDelete :boolean = false;

  constructor(private router: Router,
    private multipleDelete : MultipleDeleteService
  ){
    this.multipleDelete.canDelete.subscribe(res =>{
      this.canDelete = res;
    })
    this.multipleDelete.canUseMultipleDelete.subscribe(res => {
      this.canUseMultipleDelete = res;
    })
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

  deleteAll(){
    if(this.canDelete == false) return;
    this.multipleDelete.deleteMultiples.emit();
  }

  openNavigation(){
    this.openedNavigation = ! this.openedNavigation;
  }
}
