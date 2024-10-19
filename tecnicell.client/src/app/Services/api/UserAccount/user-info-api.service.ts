import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { UserInfo } from '../../../Interfaces/business/Models/UserAccount';

@Injectable({
  providedIn: 'root'
})
export class UserInfoApiService{

  constructor(public http: HttpClient) {}

  select(): Observable<UserInfo[]>{
    return this.http.get<UserInfo[]>(environment.url + '/api/UserInfos');
  }
  get(id:any) : Observable<UserInfo>{
      return this.http.get<UserInfo>(environment.url + '/api/UserInfos/'+id);
  }
  add(data : any){
    data.userCode = 'default';
    const user : UserInfo = this.mapperAdd(data);
    return this.http.post<UserInfo>(environment.url + '/api/UserInfos/',user);
  }
  edit(data : any){
    const user : UserInfo = this.mapperAdd(data);
    return this.http.put<UserInfo>(environment.url + '/api/UserInfos/' + user.userCode,user);
  }
  delete(data : any){
      return this.http.delete(environment.url + '/api/UserInfos/'+data.userCode);
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
