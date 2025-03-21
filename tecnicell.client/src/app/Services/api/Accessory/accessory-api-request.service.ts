import { Injectable, NgModule } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { AccessoryResponse, AccessoryView } from '../../../Interfaces/business/ApiResponses/AccessoryResponse';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Accessory, AccessoryHistory } from '../../../Interfaces/business/Models/Accessory';
import { environment } from '../../../../environments/environment';
import { AccessoryRequest } from '../../../Interfaces/business/ApiRequest/AccessoryRequest';
import { Sale, SaleViewModel } from '../../../Interfaces/business/Models/Sale';
import { AccessoryHistoryApiService } from './accessory-history-api.service';
import { SaleApiService } from '../Extras/sale-api.service';
import { Notification, NotificationType } from '../../../Components/notification-bubble/notification-bubble.component';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';
import server from '../../../Logic/ServerAdress';
import { generateDate } from '../../../Logic/ControlDate';
@Injectable({
  providedIn: 'root'
})
export class AccessoryApiRequestService implements ApiService<AccessoryView, AccessoryResponse> {

  constructor(public http: HttpClient,
    private historyApi:AccessoryHistoryApiService,
    private notificationService : NotificationSystemService,
    private authService: AuthService
  ) {
    this.authService.loadUser();
  }

  select(): Observable<AccessoryView[]>{
    return this.http.get<AccessoryView[]>(server() + '/api/Accessories');
  }
  get(id:any) : Observable<AccessoryResponse>{
      return this.http.get<AccessoryResponse>(server() + '/api/Accessories/'+id);
  }
  add(data : any){
    const req : AccessoryRequest = this.mapperToRequest(data);

    this.http.post<any>(server() + '/api/Accessories/', req.model).subscribe(
      res=>{
        data.accessoryCode = res.accessoryCode;
        data.date = new Date();
        this.historyApi.add(data);
        this.notificationService.showNotifcation("Se ha añadido un elemento con exito!", 0)
      },
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
    )
  }
  edit(data : any, id : any){
    const model : any = this.mapperToEdit(data);
    this.http.put<any>(server() + '/api/Accessories/' + data.accessoryCode, model)
            .subscribe(
              res => 
                this.notificationService.showNotifcation("Se ha editado un elemento con exito!", 0),
              err =>  this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
            );
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/Accessories/' + data.code);
  }
  mapperToRequest(data:any) : AccessoryRequest{
    const accessory : Accessory ={
      accessoryCode: 'default',
      name : data.name,
      salePrice: data.salePrice,
      accessoryType : data.accessoryType,
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
    let history : AccessoryHistory = {
      date : data.setTime == false? new Date(): generateDate(data.day,data.hours,data.minutes,data.seconds,data.time),
      userCode: this.authService.myUser.value.userCode!,
      actionHistory : data.actionHistory,
      description : data.description,
      supplierCode: data.supplierCode == 'none'? null: data.supplierCode,
      quantity : data.quantity,
      toBranch : data.branchCode
    }

    if(sale && data.sale == true){
      history.saleCodeNavigation = sale;
    }
    const request : AccessoryRequest ={
      model : accessory,
      history : history
    }
    return request;
  }
  mapperToEdit(data:any){
    const accessory : Accessory ={
      accessoryCode: data.accessoryCode,
      name : data.name,
      salePrice: data.salePrice,
      accessoryType : data.accessoryType,
    }
    return accessory
  }
}
