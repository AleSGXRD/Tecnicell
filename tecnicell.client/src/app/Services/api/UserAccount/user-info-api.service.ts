import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { UserInfo } from '../../../Interfaces/business/Models/UserAccount';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class UserInfoApiService{

  constructor(public http: HttpClient) {}

  select(): Observable<UserInfo[]>{
    return this.http.get<UserInfo[]>(server() + '/api/UserInfos');
  }
  get(id:any) : Observable<UserInfo>{
      return this.http.get<UserInfo>(server() + '/api/UserInfos/'+id);
  }
  add(data : any){
    data.userCode = 'default';
    const user : UserInfo = this.mapperAdd(data);
    return this.http.post<UserInfo>(server() + '/api/UserInfos/',user);
  }
  edit(data : any){
    const user : UserInfo = this.mapperAdd(data);
    return this.http.put<UserInfo>(server() + '/api/UserInfos/' + user.userCode,user);
  }
  delete(data : any){
      return this.http.delete(server() + '/api/UserInfos/'+data.userCode);
  }
  mapperAdd(data:any){
    const user : UserInfo ={
      role : data.role,
      name : data.name,
      branch : data.branch,
      userCode : data.userCode,
    }
    return user;
  }
}
