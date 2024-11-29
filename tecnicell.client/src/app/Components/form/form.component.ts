import { Component, input, Input } from '@angular/core';
import { FormService } from '../../Services/form/form.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormsModule, Validators, ReactiveFormsModule, FormControl, FormGroupDirective, NgForm} from '@angular/forms';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { ButtonComponent, Rounded } from "../buttons/button/button.component";
import { DialogService } from '../../Services/dialog/dialog.service';

import { ErrorStateMatcher } from '@angular/material/core';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent {

  roundedFull : Rounded = Rounded.FULL;

  @Input()
  public formService! :FormService;
  @Input()
  public dialogService! : DialogService;
  public form :any;

  public inputs : FormField[] =[
    {
      type : "text",
      formControlName:"email",
      name: "Email",
      placeholder : "...",
      fieldRequired : false,
    }
  ];

  matcher = new MyErrorStateMatcher();
  public error : boolean = false; 

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.formService.inputsField.subscribe(res=>{
      if(res != null)
        this.inputs = res
    }, err => console.log(err));
    this.formService.form.subscribe(res => {
      if(res != null)
        this.form = res
    },err => console.log(err));
  }

  DesappearForm(){
    this.error=false;
    this.formService.active = false;
  }
  SendData(){  
    this.error = false;

    this.inputs.forEach(element=> {
        if(this.form.get(element.formControlName).errors != null)
          this.error = true 
      }
    );

    if(this.error)
      return ;
    this.dialogService.SetMethod(()=>{
      this.formService.ProcessData(this.form.value);
      this.DesappearForm();
    });
  }
}
