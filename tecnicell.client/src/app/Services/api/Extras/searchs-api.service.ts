import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotificationSystemService } from '../../notification-system.service';
import { ApiService } from '../ApiService.service';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Search } from '../../../Interfaces/business/Models/Search';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class SearchsApiService implements ApiService<Search,Search> {

  constructor(public http: HttpClient,
    private notificationService:NotificationSystemService
  ) {
   }
  
   select(): Observable<Search[]>{
    return this.http.get<Search[]>(server() + '/api/Searchs');
  }
  get(id:any) : Observable<Search>{
      return this.http.get<Search>(server() + '/api/Searchs/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    this.http.post<any>(server() + '/api/Searchs/', model).subscribe(
      res=> res,
      err => err);
  }
  edit(data : any){
    const model : any = this.mapper(data);
    this.http.put<any>(server() + '/api/Searchs/' + data.SearchCode, model).subscribe(
      res=> res,
      err => err);
  }
  delete(id : any){
    return this.http.delete<any>(server() + '/api/Searchs/' + id);
  }
  mapper(data:any){
    const model : Search = {
      date : new Date(),
      value : data.value
    };
    return model
  }
}
