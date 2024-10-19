import { Injectable } from '@angular/core';
import { Phone, PhoneHistory } from '../../../Interfaces/business/Models/Phone';
import { environment } from '../../../../environments/environment';
import { reloadPage } from '../../../Logic/ReloadPage';
import { HttpClient } from '@angular/common/http';
import { PhoneHistoryApiService } from './phone-history-api.service';
import { SaleApiService } from '../Extras/sale-api.service';
import { PhoneResponse, PhoneView } from '../../../Interfaces/business/ApiResponses/PhoneResponse';
import { Observable } from 'rxjs';
import { PhoneRequest } from '../../../Interfaces/business/ApiRequest/PhoneRequest';
import { ApiService } from '../ApiService.service';
import { SaleViewModel } from '../../../Interfaces/business/Models/Sale';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';

@Injectable({
  providedIn: 'root'
})
export class PhoneApiService implements ApiService<PhoneView,PhoneResponse> {

  constructor(public http: HttpClient,
    private historyApi:PhoneHistoryApiService,
    private notificationService: NotificationSystemService,
    private authService: AuthService
  ) {
    authService.loadUser();
  }

  select(): Observable<PhoneView[]>{
    return this.http.get<PhoneView[]>(environment.url + '/api/Phones');
  }
  get(id:any) : Observable<PhoneResponse>{
      return this.http.get<PhoneResponse>(environment.url + '/api/Phones/'+id);
  }
  add(data : any){
    const req :PhoneRequest = this.mapperToRequest(data);

    this.http.post<any>(environment.url + '/api/Phones/', req.model).subscribe(
      res=>{
        data.phoneCode = res.phoneCode;
        data.date = new Date();
        this.historyApi.add(data);
      },
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
    )
  }
  edit(data : any, id : any){
    const model : any = this.mapperToEdit(data);
    this.http.put<any>(environment.url + '/api/Phones/' + data.phoneCode, model)
            .subscribe(
              res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
              err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
            );
  }
  delete(data : any){
    return this.http.delete<any>(environment.url + '/api/Phones/' + data.code);
  }
  mapperToRequest(data:any) :PhoneRequest{
    const phone : Phone ={
      imei: data.imei,
      name:data.name,
      salePrice: data.salePrice,
      brand : data.brand,
    }
    console.log(data);
    let sale!: SaleViewModel;
    if(data.sale == true){
      if(data.currencyCode != undefined && data.cost != undefined){
            sale = {
              saleCode :'default',
              currencyCode : data.currencyCode,
              cost : data.cost
            }
            if(data.warranty != undefined)
              sale.warranty = data.warranty
      } 
    }
    let history :PhoneHistory = {
      date : new Date(),
      userCode: this.authService.myUser.value.userCode!,
      actionHistory : data.actionHistory,
      description : data.description,
      toBranch : data.toBranch
    }
    console.log(history);
    if(sale && data.sale == true){
      history.saleCodeNavigation = sale;
    }
    const request :PhoneRequest ={
      model : phone,
      history : history
    }
    console.log(request);
    return request;
  }
  mapperToEdit(data:any){
    const phone :Phone ={
      imei: data.imei,
      name:data.name,
      salePrice: data.salePrice,
      brand : data.brand,
    }
    return phone
  }
}
