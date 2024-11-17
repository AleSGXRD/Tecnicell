import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { Branch } from '../../../Interfaces/business/Models/Branch';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { NotificationSystemService } from '../../notification-system.service';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class BranchApiService implements ApiService<Branch,Branch>{

  constructor(public http: HttpClient, private notificationService: NotificationSystemService) {}

  select(): Observable<Branch[]>{
    return this.http.get<Branch[]>(server() + '/api/Branches');
  }
  get(id:any) : Observable<Branch>{
      return this.http.get<Branch>(server() + '/api/Branches/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(server() + '/api/Branches/', model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  edit(data : any){
    const model : any = this.mapper(data);
    this.http.put<any>(server() + '/api/Branches/' + data.branchCode, model).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1));
  }
  delete(id : any){
    return this.http.delete<any>(server() + '/api/Branches/' + id);
  }
  mapper(data:any) : any{
    if(data.branchCode == undefined){
      data.branchCode = "default"
    }

    const model : Branch ={
      branchCode : data.branchCode,
      name : data.name
    }
    return model;
  }
}
