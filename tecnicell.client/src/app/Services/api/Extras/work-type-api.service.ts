import { Injectable } from '@angular/core';
import { WorkType } from '../../../Interfaces/business/Models/WorkType';
import server from '../../../Logic/ServerAdress';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { NotificationSystemService } from '../../notification-system.service';

@Injectable({
  providedIn: 'root'
})
export class WorkTypeApiService {

  constructor(public http: HttpClient, private notificationService: NotificationSystemService) {}

  select(): Observable<WorkType[]>{
    return this.http.get<WorkType[]>(server() + '/api/WorkTypes');
  }
  get(id:any) : Observable<WorkType>{
    return this.http.get<WorkType>(server() + '/api/WorkTypes/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(server() + '/api/WorkTypes/', model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  edit(data : any){
    const model : any = this.mapper(data);
    this.http.put<any>(server() + '/api/WorkTypes/' + data.name, model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1));
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/WorkTypes/' + data.name);
  }
  mapper(data:any) : any{
    const model : WorkType ={
      name:data.name
    }
    return model;
  }
}
