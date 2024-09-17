import { Component, Input } from '@angular/core';
import { FormService } from '../../services/form/form.service';
import { FormType } from '../../types/tools/Form/FormType';
import { Rounded } from '../buttons/button/button.component';
import { FormGroup } from '@angular/forms';

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
  service! : FormService;

  ActiveForm(){
    this.formAdd.reset();
    this.service.SetInputsField(this.inputs);
    this.service.SetFormAndActive(FormType.ADD, this.formAdd);
  }
}
