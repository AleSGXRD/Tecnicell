import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  public dialogDeleteMethod:Function =()=>{};
  public dialogDeleteActive:boolean = false;

  public dialogMethod:Function =()=>{};
  public dialogActive:boolean = false;

  constructor() { }

  public SetMethod(action: Function){
    this.dialogMethod = action;
    this.dialogActive = true;
  }
  public ExecuteMethod(){
    this.dialogMethod();
    this.RemoveMethod();
  }
  public RemoveMethod(){
    this.dialogMethod = ()=>{};
    this.dialogActive = false;
  }

  public SetDeleteMethod(action: Function){
    this.dialogDeleteMethod = action;
    this.dialogDeleteActive = true;
  }
  public ExecuteDeleteMethod(){
    this.dialogDeleteMethod();
    this.RemoveDeleteMethod();
  }
  public RemoveDeleteMethod(){
    this.dialogDeleteMethod = ()=>{};
    this.dialogDeleteActive = false;
  }
}
