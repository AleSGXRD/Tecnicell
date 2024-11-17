import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { UserAccount } from '../../../Interfaces/business/Models/UserAccount';
import { HttpClient } from '@angular/common/http';
import { NotificationSystemService } from '../../notification-system.service';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { UserInfoApiService } from './user-info-api.service';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class UserAccountApiService implements ApiService<UserAccount,UserAccount> {

  constructor(public http: HttpClient,
     private notificationService : NotificationSystemService, 
    private userInfoApiService: UserInfoApiService) {}

  select(): Observable<UserAccount[]>{
    return this.http.get<UserAccount[]>(server() + '/api/UserInfos');
  }
  get(id:any) : Observable<UserAccount>{
      return this.http.get<UserAccount>(server() + '/api/UserInfos/'+id);
  }
  add(data : any){
    const account = this.mapperAdd(data);
    this.userInfoApiService.add(data).subscribe(
      res =>{
        account.userCode = res.userCode;
        this.http.post(server() + '/api/UserAccounts/',account).subscribe(
          res => this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
          err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1))
      },
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
    )
  }
  edit(data : any){
    const account : UserAccount = this.mapperEdit(data);
    this.userInfoApiService.edit(data).subscribe(
      res =>{
        this.http.put<UserAccount>(server() + '/api/UserAccounts/' + account.userCode,account).subscribe(
          res => this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
          err => this.notificationService.showNotifcationWithError("Ha ocurrido un error al intentar editar el elemento.", 1,err))
      },
      err => this.notificationService.showNotifcationWithError("Ha ocurrido un error al intentar editar el elemento.", 1,err)
    )
  }
  delete(data : any){
      return this.http.delete(server() + '/api/UserInfos/'+data.userCode);
  }
  mapperAdd(data:any){
    const account : UserAccount = {
      name : data.name,
      password : data.password
    }
    return account
  }
  mapperEdit(data:any){
    const account : UserAccount = {
      userCode : data.userCode,
      name : data.name,
      password : data.password
    }
    return account
  }
}
