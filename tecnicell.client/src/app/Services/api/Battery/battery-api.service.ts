import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BatteryHistoryApiService } from './battery-history-api.service';
import { SaleApiService } from '../Extras/sale-api.service';
import { BatteryResponse, BatteryView } from '../../../Interfaces/business/ApiResponses/BatteryResponse';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { ApiService } from '../ApiService.service';
import { reloadPage } from '../../../Logic/ReloadPage';
import { Battery, BatteryHistory } from '../../../Interfaces/business/Models/Battery';
import { BatteryRequest } from '../../../Interfaces/business/ApiRequest/BatteryRequest';
import { SaleViewModel } from '../../../Interfaces/business/Models/Sale';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';

@Injectable({
  providedIn: 'root'
})
export class BatteryApiService implements ApiService<BatteryView, BatteryResponse> {

  constructor(public http: HttpClient,
    private historyApi:BatteryHistoryApiService,
    private notificationService: NotificationSystemService,
    private authService: AuthService
  ) {
    authService.loadUser();
  }

  select(): Observable<BatteryView[]>{
    return this.http.get<BatteryView[]>(environment.url + '/api/Batteries');
  }
  get(id:any) : Observable<BatteryResponse>{
      return this.http.get<BatteryResponse>(environment.url + '/api/Batteries/'+id);
  }
  add(data : any){
    const req : BatteryRequest = this.mapperToRequest(data);

    this.http.post<any>(environment.url + '/api/Batteries/', req.model).subscribe(
      res=>{
        data.batteryCode = res.batteryCode;
        data.date = new Date();
        this.historyApi.add(data);
      },
      err => this.notificationService.showNotifcationWithError("Ha ocurrido un error al intentar añadir el elemento.", 1,err)
    )
  }
  edit(data : any, id : any){
    const model : any = this.mapperToEdit(data);
    this.http.put<any>(environment.url + '/api/Batteries/' + data.batteryCode, model)
            .subscribe(
              res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
              err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
            );
  }
  delete(data : any){
    return this.http.delete<any>(environment.url + '/api/Batteries/' + data.code);
  }
  mapperToRequest(data:any) : BatteryRequest{
    const battery : Battery ={
      batteryCode: 'default',
      warranty : data.warrantyBattery,
      name : data.name,
      salePrice: data.salePrice,
      brand : data.brand,
    }
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
    let history : BatteryHistory = {
      date : new Date(),
      userCode: this.authService.myUser.value.userCode!,
      actionHistory : data.actionHistory,
      description : data.description,
      quantity : data.quantity,
      toBranch : data.toBranch
    }
    console.log(history);
    if(sale && data.sale == true){
      history.saleCodeNavigation = sale;
    }
    const request : BatteryRequest ={
      model : battery,
      history : history
    }
    console.log(request);
    return request;
  }
  mapperToEdit(data:any){
    const battery : Battery ={
      batteryCode: data.batteryCode,
      name : data.name,
      salePrice: data.salePrice,
      warranty : data.warranty,
      brand : data.brand,
    }
    return battery
  }
}
