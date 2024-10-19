import { Component, Input } from '@angular/core';
import { FormService } from '../../Services/form/form.service';
import { FormType } from '../../Interfaces/tools/Form/FormType';
import { Rounded } from '../buttons/button/button.component';
import { FormGroup } from '@angular/forms';
import { ApiService } from '../../Services/api/ApiService.service';
import { FilterField } from '../../Interfaces/tools/Filters/Filters';
import { FilterTableService } from '../../Services/Filter/filter-table.service';
import { Router } from '@angular/router';

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

  constructor(private router: Router){

  }

  ActiveForm(){
    this.formAdd.reset();
    this.service.SetInputsField(this.inputs);
    this.service.SetFormAndActive(FormType.ADD, this.formAdd, this.apiService);
  }
  Navigate(){
    console.log(this.histories)
    this.router.navigate([this.histories]);
  }

  change(){
    this.filterService.emit(this.filtersOptions);
  }
}
