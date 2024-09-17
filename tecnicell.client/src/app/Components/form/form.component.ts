import { Component, input, Input } from '@angular/core';
import { FormService } from '../../services/form/form.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormsModule, Validators, ReactiveFormsModule} from '@angular/forms';
import { FormField } from '../../types/tools/Form/FormField';
import { ButtonComponent, Rounded } from "../buttons/button/button.component";
import { DialogService } from '../../services/dialog/dialog.service';


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
      placeholder : "..."
    }
  ];

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
    this.inputs.forEach(element=> {
      if(this.form.get(element.formControlName).errors != null)
       this.error = true
      }
    );
    if(this.error == true)
      return ;
    this.dialogService.SetMethod(()=>{
      this.formService.ProcessData(this.form.value);
      this.DesappearForm();
    });
  }
}
