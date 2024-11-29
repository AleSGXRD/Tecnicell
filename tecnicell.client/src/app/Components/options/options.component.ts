import { Component, Input } from '@angular/core';
import { FormService } from '../../Services/form/form.service';
import { FormType } from '../../Interfaces/tools/Form/FormType';
import { Rounded } from '../buttons/button/button.component';
import { FormGroup } from '@angular/forms';
import { ApiService } from '../../Services/api/ApiService.service';
import { FilterField } from '../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../Services/Filter/filter-table.service';
import { Router } from '@angular/router';
import { MultipleDeleteService } from '../../Services/multiple-delete.service';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrl: './options.component.css'
})
export class OptionsComponent {
  @Input()
  deleteMethod! : Function;

  buttonRounded : Rounded = Rounded.FULL;
  @Input()
  inputs:any;
  @Input()
  formAdd! : FormGroup;

  @Input()
  filtersOptions! : FilterField[];
  @Input()
  filterService! : FilterTableService;

  @Input()
  service! : FormService;
  @Input()
  apiService! : ApiService<any,any>
  
  @Input()
  histories! : string ;

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
    this.formAdd.reset();
    this.service.SetInputsField(this.inputs);
    this.service.SetFormAndActive(FormType.ADD, this.formAdd, this.apiService);
  }
  Navigate(){
    this.router.navigate([this.histories]);
  }

  change(){
    this.filterService.emit(this.filtersOptions);
  }
  deleteAll(){
    if(this.canDelete == false) return;
    this.multipleDelete.deleteMultiples.emit();
  }
}
