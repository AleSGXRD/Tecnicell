import { Injectable } from '@angular/core';
import { AuthRequest } from '../../../Interfaces/business/Authorization/AuthRequest';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Response } from '../../../Interfaces/business/Authorization/Response';
import {  UserInfo } from '../../../Interfaces/business/Models/UserAccount';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public myUser : BehaviorSubject<UserInfo> = new BehaviorSubject<UserInfo>({
    role : "default",
    branch : "default"
  });

  constructor(public http: HttpClient,
    private router : Router
  ) {
  }

  auth(user:AuthRequest) {
      return this.http.post<Response>(server() + '/api/UserAuthorization/login', user);
  }

  getUser(userCode:string){
    return this.http.get<UserInfo>(server() + '/api/UserInfos/' + userCode);
  }

  loadUser(){
    if(this.myUser.value.role == "default"){
      this.getUser(this.dataFromLocalStorage.code).subscribe(
        res=> this.myUser.next(res),
        err =>{ 
          this.myUser.next({
            role : "default",
            branch : "default"
          });
          this.router.navigate(['/login'])
        }
      )
    }
    else if(this.myUser.value.name != this.name){
      this.getUser(this.dataFromLocalStorage.code).subscribe(
        res=> this.myUser.next(res),
        err =>{ 
          this.myUser.next({
            role : "default",
            branch: "default"
          });
          this.router.navigate(['/login'])
        }
      )
    }
  }
  
  get logged(){
    return !!localStorage.getItem('user');
  }

  get name(){
    return this.dataFromLocalStorage.code;
  }
  

  get token(){
    return this.dataFromLocalStorage.token;
  }

  get dataFromLocalStorage(){
    const data = localStorage?.getItem('user');
    if(!data) return '';
    return JSON.parse(data);
  }
}
