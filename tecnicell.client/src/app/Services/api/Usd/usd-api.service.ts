import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Usd } from '../../../Interfaces/business/Models/Usd';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Sale } from '../../../Interfaces/business/Models/Sale';
import { ApiService } from '../ApiService.service';
import { NotificationSystemService } from '../../notification-system.service';

@Injectable({
  providedIn: 'root'
})
export class UsdApiService implements ApiService<Usd,Usd> {

  usd : BehaviorSubject<Usd> = new BehaviorSubject<Usd>({
    date : new Date(),
    value : 0
  });

  constructor(public http: HttpClient,
    private notificationService:NotificationSystemService
  ) {
    this.getLast();
   }
  
   select(): Observable<Usd[]>{
    return this.http.get<Usd[]>(environment.url + '/api/Usds');
  }
  get(id:any) : Observable<Usd>{
      return this.http.get<Usd>(environment.url + '/api/Usds/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(environment.url + '/api/Usds/', model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  edit(data : any){
    const model : any = this.mapper(data);
    this.http.put<any>(environment.url + '/api/Usds/' + data.UsdCode, model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  delete(id : any){
    return this.http.delete<any>(environment.url + '/api/Usds/' + id);
  }

  mapper(data:any) : any{
    const model : Usd ={
      date : new Date(),
      value : data.value
    }
    return model;
  }
  getLast() {
    this.http.get<Usd>(environment.url + '/api/Usds/last').subscribe(res => this.usd.next(res));
  }

  getSale(quantity : number, price: number, warranty: number){
    if(this.usd.value.value == 0) return undefined;

    var date = new Date();
    date.setDate(date.getDate() + warranty);

    const sale : Sale ={
      currencyCode: "pN52_BZXv_Due0_4Gma",
      cost : (this.usd.value.value * price) * quantity,
      warranty : (warranty == 0? undefined : date)
    };
    return sale
  }
  getSaleEdit(quantity : number, price: number, warranty: Date){
    if(this.usd.value.value == 0) return undefined;


    const sale : Sale ={
      currencyCode: "pN52_BZXv_Due0_4Gma",
      cost : (this.usd.value.value * price) * quantity,
      warranty : warranty
    };
    return sale
  }
}
