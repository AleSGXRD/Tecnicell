import { Component } from '@angular/core';
import { UsdApiService } from '../../../Services/api/Usd/usd-api.service';
import { DialogService } from '../../../Services/dialog/dialog.service';
import { FormService } from '../../../Services/form/form.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormField } from '../../../Interfaces/tools/Form/FormField';
import { FormType } from '../../../Interfaces/tools/Form/FormType';
import { Usd } from '../../../Interfaces/business/Models/Usd';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  direction : string[]=['Main', "Dashboard"];

  usdChange! : Usd;

  constructor(
    public dialogService : DialogService,
    public formService: FormService,
    public apiService: UsdApiService,
    private formBuilder : FormBuilder,

    private usdService : UsdApiService){
    this.usdService.usd.subscribe(res => this.usdChange = res);
    this.usdService.getLast();
  }

  editUsd(){
    const form  = this.formBuilder.nonNullable.group({
      value: [undefined, [Validators.required]]
    })
    const inputs :FormField[]= [
      {
        type : "number",
        formControlName:"value",
        name: "Valor del dolar.",
        placeholder : "0",
        fieldRequired : true,
        errors : [{
          type : 'required',
          message: 'Se necesita rellenar este campo'
        }],
      },
    ]

    this.formService.SetInputsField(inputs);
    this.formService.SetFormAndActive(FormType.ADD, form, this.apiService);
  }
}
