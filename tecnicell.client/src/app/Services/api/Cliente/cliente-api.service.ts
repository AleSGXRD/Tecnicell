import { Injectable, Injector } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ClienteApiService implements ApiService {
  public http:HttpClient;

  constructor(private injector:Injector){
      this.http = injector.get(HttpClient);
  }

  select(){

  }
  add(data : any){
      
  }
  edit(data : any, index : number){

  }
  delete(index : number){
      console.log('delete: ' + index);
  }

}
