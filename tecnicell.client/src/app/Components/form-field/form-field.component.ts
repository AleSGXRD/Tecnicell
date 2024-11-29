import { Component, Input, signal, SimpleChanges } from '@angular/core';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { ErrorStateMatcher } from '@angular/material/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-form-field',
  templateUrl: './form-field.component.html',
  styleUrl: './form-field.component.css'
})
export class FormFieldComponent {
  @Input()
  formField!: FormField;
  @Input()
  matcher!: ErrorStateMatcher;
  @Input()
  form! : any;

  checked = false;
  protected readonly value = signal('');

  protected onInput(event: Event) {
    this.value.set((event.target as HTMLInputElement).value);
  }
  onInputLimit(event: Event, controlName: string) {
    const input = event.target as HTMLInputElement;
    if(controlName ==='hours'){
      if(parseInt(input.value) > 12){
        input.value = '12';
      }
    }
    if(controlName ==='minutes' || controlName ==='seconds'){
      if(parseInt(input.value) > 60){
        input.value = '60';
      }
    }
    if (input.value.length > 2) {
      input.value = input.value.slice(0, 2); // Limitar a 2 caracteres
    }
    this.form.get(controlName)?.setValue(input.value); // Actualizar el valor en el control del formulario
  }
  

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
}
