import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { AccessoryType } from '../../../Interfaces/business/Models/Accessory';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { NotificationSystemService } from '../../notification-system.service';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class AccessoryTypeApiService implements ApiService<AccessoryType,AccessoryType> {

  constructor(public http: HttpClient, private notificationService: NotificationSystemService) {}

  select(): Observable<AccessoryType[]>{
    return this.http.get<AccessoryType[]>(server() + '/api/AccessoryTypes');
  }
  get(id:any) : Observable<AccessoryType>{
      return this.http.get<AccessoryType>(server() + '/api/AccessoryTypes/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(server() + '/api/AccessoryTypes/', model)
      .subscribe(
        res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
        err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
      );
  }
  edit(data : any, id : any){
    const model : any = this.mapper(data);
    this.http.put<any>(server() + '/api/AccessoryTypes/' + data.accessoryCode, model)
    .subscribe(
      res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
    );
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/AccessoryTypes/' + data.accessoryTypeCode);
  }
  mapper(data:any) : any{
    if(data.accessoryTypeCode == undefined){
      data.accessoryTypeCode = "default";
    }

    const model : AccessoryType ={
      accessoryTypeCode : data.accessoryTypeCode,
      name : data.name
    }
    return model;
  }
}
