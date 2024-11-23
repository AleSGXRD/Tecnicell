import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { Sale } from '../../../Interfaces/business/Models/Sale';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class SaleApiService implements ApiService<Sale,Sale> {

  constructor(public http: HttpClient) {}

  select(): Observable<Sale[]>{
    return this.http.get<Sale[]>(server() + '/api/Sales');
  }
  get(id:any) : Observable<Sale>{
      return this.http.get<Sale>(server() + '/api/Sales/'+id);
  }
  add(data : any){
    const model : any = this.mapper(data);
    return this.http.post<any>(server() + '/api/Sales/', model);
  }
  edit(data : any, id : any){
    const model : any = this.mapper(data);
    return this.http.put<any>(server() + '/api/Sales/' + model.saleCode, model);
  }
  delete(id : any){
    return this.http.delete<any>(server() + '/api/Sales/' + id);
  }
  mapper(data:any) : any{
    const model : Sale ={
      saleCode: data.saleCode,
      currencyCode : data.currencyCode,
      warranty : data.warranty,
      cost: data.cost,
    }
    return model;
  }
}
