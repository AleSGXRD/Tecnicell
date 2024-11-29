import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ScreenResponse, ScreenView } from '../../../Interfaces/business/ApiResponses/ScreenResponse';
import { ApiService } from '../ApiService.service';
import { HttpClient } from '@angular/common/http';
import { ScreenHistoryApiService } from './screen-history-api.service';
import { SaleApiService } from '../Extras/sale-api.service';
import { Observable } from 'rxjs';
import { ScreenRequest } from '../../../Interfaces/business/ApiRequest/ScreenRequest';
import { Screen, ScreenHistory } from '../../../Interfaces/business/Models/Screen';
import { SaleViewModel } from '../../../Interfaces/business/Models/Sale';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';
import server from '../../../Logic/ServerAdress';
import { generateDate } from '../../../Logic/ControlDate';

@Injectable({
  providedIn: 'root'
})
export class ScreenApiService implements ApiService<ScreenView, ScreenResponse> {

  constructor(public http: HttpClient,
    private historyApi:ScreenHistoryApiService,
    private notificationService : NotificationSystemService,
    private authService: AuthService
  ) {
    authService.loadUser();
  }

  select(): Observable<ScreenView[]>{
    return this.http.get<ScreenView[]>(server() + '/api/Screens');
  }
  get(id:any) : Observable<ScreenResponse>{
      return this.http.get<ScreenResponse>(server() + '/api/Screens/'+id);
  }
  add(data : any){
    
    const req : ScreenRequest = this.mapperToRequest(data);

    this.http.post<any>(server() + '/api/Screens/', req.model).subscribe(
      res=>{
        data.screenCode = res.screenCode;
        data.date = new Date();
        this.historyApi.add(data);
      },
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
    )
  }
  edit(data : any, id : any){
    const model : any = this.mapperToEdit(data);
    this.http.put<any>(server() + '/api/Screens/' + data.screenCode, model)
            .subscribe(
              res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
              err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
            );
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/Screens/' + data.code);
  }
  mapperToRequest(data:any) : ScreenRequest{
    const screen : Screen ={
      screenCode: 'default',
      name : data.name,
      salePrice: data.salePrice,
      brand : data.brand,
      warranty : data.warrantyScreen
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
    let history : ScreenHistory = {
      date :data.setTime == false? new Date() : generateDate(data.day,data.hours,data.minutes,data.seconds,data.time),
      userCode: this.authService.myUser.value.userCode!,
      actionHistory : data.actionHistory,
      description : data.description,
      quantity : data.quantity,
      supplierCode: data.supplierCode == 'none'? null: data.supplierCode,
      toBranch : data.toBranch
    }
    if(sale && data.sale == true){
      history.saleCodeNavigation = sale;
    }
    const request : ScreenRequest ={
      model : screen,
      history : history
    }
    return request;
  }
  mapperToEdit(data:any){
    const screen : Screen ={
      screenCode: data.screenCode,
      name : data.name,
      salePrice: data.salePrice,
      brand : data.brand,
      warranty : data.warranty
    }
    return screen
  }
}
