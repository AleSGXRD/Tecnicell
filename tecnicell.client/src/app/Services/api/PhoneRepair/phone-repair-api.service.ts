  import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { PhoneRepairResponse, PhoneRepairView } from '../../../Interfaces/business/ApiResponses/PhoneRepairResponse';
import { HttpClient } from '@angular/common/http';
import { PhoneRepairHistoryApiService } from './phone-repair-history-api.service';
import { SaleApiService } from '../Extras/sale-api.service';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { PhoneRepairRequest } from '../../../Interfaces/business/ApiRequest/PhoneRepairRequest';
import { PhoneRepair, PhoneRepairHistory } from '../../../Interfaces/business/Models/PhoneRepair';
import { SaleViewModel } from '../../../Interfaces/business/Models/Sale';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class PhoneRepairApiService implements ApiService<PhoneRepairView,PhoneRepairResponse> {

  constructor(public http: HttpClient,
    private historyApi:PhoneRepairHistoryApiService,
    private notificationService: NotificationSystemService,
    private authService: AuthService
  ) {
    authService.loadUser();
  }

  select(): Observable<PhoneRepairView[]>{
    return this.http.get<PhoneRepairView[]>(server() + '/api/PhoneRepairs');
  }
  get(id:any) : Observable<PhoneRepairResponse>{
      return this.http.get<PhoneRepairResponse>(server() + '/api/PhoneRepairs/'+id);
  }
  add(data : any){
    const req :PhoneRepairRequest = this.mapperToRequest(data);

    this.http.post<any>(server() + '/api/PhoneRepairs/', req.model).subscribe(
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
    this.http.put<any>(server() + '/api/PhoneRepairs/' + data.imei, model)
            .subscribe(
            res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
            );
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/PhoneRepairs/' + data.code);
  }
  mapperToRequest(data:any) :PhoneRepairRequest{
    const phone : PhoneRepair ={
      imei: data.imei,
      brand : data.brand,
      name : data.name,
      customerId: data.customerId,
      customerName: data.customerName,
      customerNumber: data.customerNumber,
      price: data.price,
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
    let history :PhoneRepairHistory = {
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
    const request :PhoneRepairRequest ={
      model : phone,
      history : history
    }
    console.log(request);
    return request;
  }
  mapperToEdit(data:any){
    const phone :PhoneRepair ={
      imei: data.imei,
      name: data.name,
      brand : data.brand,
      customerId: data.customerId,
      customerName : data.customerName,
      customerNumber : data.customerNumber,
      price: data.price,
    }
    return phone
  }
}
