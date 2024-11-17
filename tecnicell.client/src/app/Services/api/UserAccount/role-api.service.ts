import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { Role } from '../../../Interfaces/business/Models/Role';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NotificationSystemService } from '../../notification-system.service';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class RoleApiService implements ApiService<Role,Role> {

  constructor(public http: HttpClient,
    private notificationService : NotificationSystemService
  ) {}

  select(): Observable<Role[]>{
    return this.http.get<Role[]>(server() + '/api/Roles');
  }
  get(id:any) : Observable<Role>{
      return this.http.get<Role>(server() + '/api/Roles/'+id);
  }
  add(data : any){
    this.http.post(server() + '/api/Roles/',data).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1))
  }
  edit(data : any){
    this.http.put(server() + '/api/Roles/' + data.roleCode,data).subscribe(
      res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
  }
  delete(data : any){
      return this.http.delete(server() + '/api/Roles/'+data.roleCodes);
  }
}
