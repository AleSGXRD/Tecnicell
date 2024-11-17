import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Supplier } from '../../../Interfaces/business/Models/Supplier';
import server from '../../../Logic/ServerAdress';
import { Observable } from 'rxjs';
import { NotificationSystemService } from '../../notification-system.service';

@Injectable({
  providedIn: 'root'
})
export class SupplierApiService {

  constructor(public http: HttpClient, private notificationService: NotificationSystemService) {}

  select(): Observable<Supplier[]>{
    return this.http.get<Supplier[]>(server() + '/api/Suppliers');
  }
  get(id:any) : Observable<Supplier>{
    return this.http.get<Supplier>(server() + '/api/Suppliers/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(server() + '/api/Suppliers/', model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  edit(data : any){
    const model : any = this.mapper(data);
    this.http.put<any>(server() + '/api/Suppliers/' + data.supplierCode, model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1));
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/Suppliers/' + data.supplierCode);
  }
  mapper(data:any) : any{
    if(data.supplierCode == undefined){
      data.supplierCode = "default";
    }

    const model : Supplier ={
      supplierCode : data.supplierCode,
      name : data.name
    }
    return model;
  }
}
