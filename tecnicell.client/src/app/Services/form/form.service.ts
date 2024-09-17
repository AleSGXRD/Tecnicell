import { Injectable } from '@angular/core';
import { FormType } from '../../types/tools/Form/FormType';
import { BehaviorSubject } from 'rxjs';
import { FormBuilder } from '@angular/forms';
import { FormField } from '../../types/tools/Form/FormField';
import { ApiService } from '../api/ApiService.service';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  public active : boolean = false;
  public apiService! : ApiService<any>;

  public form: BehaviorSubject<any>;
  public inputsField: BehaviorSubject<any>;

  public formType : FormType = FormType.ADD;
  public idEditting : any;

  constructor(private formBuilder: FormBuilder) {
    this.inputsField = new BehaviorSubject([]);
    this.form = new BehaviorSubject<any>(this.formBuilder.nonNullable.group(
      {
        name:['']
      }
    ));
  }

  public SetActiveForm(active : boolean){
    this.active = active;
  }
  public SetForm(form:any){
    this.form.next(form);
  }
  public SetInputsField(inputs : FormField[]){
    this.inputsField.next(inputs);
  }
  public SetFormAndActive(formType: FormType, form:any){
    this.formType = formType;
    this.form.next(form);
    this.SetActiveForm(true);
  }

  public ProcessData(data:any){
    switch(this.formType){
      case FormType.ADD:
        this.AddData(data);
        break;
      case FormType.EDIT:
        this.UpdateData(data);
        break;
    }
  }

  public AddData(data : any){
    this.apiService.add(data).subscribe(res => console.log(res), err => console.log(err));
  }
  public UpdateData(data:any){
    this.apiService.edit(data,this.idEditting).subscribe(res=> console.log(res), err => console.log(err));
  }
}

