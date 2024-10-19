import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Currency } from '../../../Interfaces/business/Models/Currency';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { NotificationSystemService } from '../../notification-system.service';

@Injectable({
  providedIn: 'root'
})
export class CurrencyApiService {

  constructor(public http: HttpClient, private notificationService: NotificationSystemService) {}

  select(): Observable<Currency[]>{
    return this.http.get<Currency[]>(environment.url + '/api/Currencies');
  }
  get(id:any) : Observable<Currency>{
    return this.http.get<Currency>(environment.url + '/api/Currencies/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(environment.url + '/api/Currencies/', model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  edit(data : any){
    const model : any = this.mapper(data);
    this.http.put<any>(environment.url + '/api/Currencies/' + data.currencyCode, model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1));
  }
  delete(data : any){
    return this.http.delete<any>(environment.url + '/api/Currencies/' + data.currencyCode);
  }
  mapper(data:any) : any{
    if(data.currencyCode == undefined){
      data.currencyCode = "default";
    }

    const model : Currency ={
      currencyCode : data.currencyCode,
      currencyName : data.currencyName
    }
    return model;
  }
}
