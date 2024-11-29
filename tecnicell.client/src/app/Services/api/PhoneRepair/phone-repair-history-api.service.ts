import { Injectable } from '@angular/core';
import { PhoneRepairHistory } from '../../../Interfaces/business/Models/PhoneRepair';
import { ApiService } from '../ApiService.service';
import { HttpClient } from '@angular/common/http';
import { SaleApiService } from '../Extras/sale-api.service';
import { PhoneHistory } from '../../../Interfaces/business/Models/Phone';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { Sale } from '../../../Interfaces/business/Models/Sale';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';
import { UsdApiService } from '../Usd/usd-api.service';
import server from '../../../Logic/ServerAdress';
import { generateDate } from '../../../Logic/ControlDate';

@Injectable({
  providedIn: 'root'
})
export class PhoneRepairHistoryApiService implements ApiService<PhoneRepairHistory, PhoneRepairHistory[]> {


  constructor(public http: HttpClient,
    private saleApi : SaleApiService,
    private notificationService: NotificationSystemService,
    private authService: AuthService,
    private usdService: UsdApiService
  ) {
    authService.loadUser();
  }

  select(): Observable<PhoneRepairHistory[]>{
    return this.http.get<PhoneRepairHistory[]>(server() + '/api/PhoneRepairHistories');
  }
  get(id:any) : Observable<PhoneRepairHistory[]>{
      return this.http.get<PhoneRepairHistory[]>(server() + '/api/PhoneRepairHistories/'+id);
  }
  add(data : any){
    let req : any = this.mapperAdd(data);
    let sale : Sale | undefined;
    if(data.sale == true){
      sale = req.saleCodeNavigation
    }
    else{
      if(req.actionHistory == "Salida" && (data.currencyCode != undefined && data.currencyCode != 'none' || data.currencyCode == undefined)){
        sale = this.usdService.getSaleInMN(1, data.salePrice, 0);
      }
      else
      {
        sale = req.saleCodeNavigation
      }
    }
    if(sale != undefined){
      this.saleApi.add(sale).subscribe(
        res => 
        {
          req.saleCode = res.saleCode;
          req.saleCodeNavigation = undefined;
  
          this.http.post<PhoneRepairHistory>(server() + '/api/PhoneRepairHistories/', req).subscribe(
            res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
          );
        }
      )
    }
    else{
      req.saleCode = undefined;
      req.saleCodeNavigation = undefined;
      this.http.post<PhoneRepairHistory>(server() + '/api/PhoneRepairHistories/', req).subscribe(
        res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
        err =>this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
      );
    }
  }
  edit(data : any, id : any){
    const model : any = this.mapperEdit(data);
    const ids = [data.imei, data.date];
    if(data.sale == true){
      if(model.saleCode != null){
        this.saleApi.edit(model.saleCodeNavigation, model.saleCode)
        .subscribe(res => {
          this.http.put<any>(server() + '/api/PhoneRepairHistories/' 
            +ids[0] + '%2C' + ids[1]+'?imei=' +ids[0] +"&date=" + ids[1], model).subscribe(
              res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
              err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
            }
        )
      }
      else{
        if(model.saleCodeNavigation.currencyCode != 'none'){
          this.saleApi.add(model.saleCodeNavigation).subscribe(
            res => {
              model.saleCode = res.saleCode;
              model.saleCodeNavigation = null;
              this.http.put<any>(server() + '/api/PhoneRepairHistories/' 
                +ids[0] + '%2C' + ids[1]+'?imei=' +ids[0] +"&date=" + ids[1], model).subscribe(
                  res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
                  err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
          })
        }
        else{
          this.http.put<any>(server() + '/api/PhoneRepairHistories/' 
            +ids[0] + '%2C' + ids[1]+'?imei=' +ids[0] +"&date=" + ids[1], model).subscribe(
              res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
              err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
        }
      }
    }
    else {
      let sale : any;
      if(model.saleCode != undefined){
        sale = model.saleCodeNavigation;
        this.saleApi.edit(sale, model.saleCode)
          .subscribe(res => {
            this.http.put<any>(server() + '/api/PhoneRepairHistories/' 
              +ids[0] + '%2C' + ids[1]+'?imei=' +ids[0] +"&date=" + ids[1], model).subscribe(
                res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
                err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
              }
            ,err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
          )
      }
      else
      {
        sale = undefined;
        this.http.put<any>(server() + '/api/PhoneRepairHistories/' 
          +ids[0] + '%2C' + ids[1]+'?imei=' +ids[0] +"&date=" + ids[1], model).subscribe(
            res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
        
      }
    }
  }
  delete(data : any){
    const ids = [data.imei, data.date];
    return this.http.delete<any>(server() + '/api/PhoneRepairHistories/'
                      +ids[0] + '%2C' + ids[1]+'?imei=' +ids[0] +"&date=" + ids[1]);
  }
  mapperAdd(data:any){
    if(data.actionHistory != "Transferido desde otra sucursal" && data.actionHistory != "Transferido hacia otra sucursal"){
      data.toBranch = this.authService.myUser.value.branch;
    }
    let model : PhoneRepairHistory = {
      imei : data.imei,
      userCode: this.authService.myUser.value.userCode!,
      date : data.setTime == false? data.date : generateDate(data.day,data.hours,data.minutes,data.seconds,data.time),
      actionHistory : data.actionHistory,
      description: data.description,
      toBranch : data.toBranch,
      saleCode : "default",
    };
    if(data.currencyCode != undefined&&data.currencyCode == 'none'){
      model.saleCodeNavigation = {
        currencyCode : undefined,
        cost : undefined,
        warranty : undefined
      }
    }
    else{
      model.saleCodeNavigation = {
        currencyCode : data.currencyCode ?? null,
        cost : data.cost ?? null,
        warranty:  data.warranty ?? null
      }
    }
    return model
  }
  mapperEdit(data:any){
    if(data.toBranch == undefined && (data.actionHistory != "Transferido desde otra sucursal" && data.actionHistory != "Transferido hacia otra sucursal")){
      data.toBranch = this.authService.myUser.value.branch;
    }
    let model : PhoneRepairHistory = {
      imei : data.imei,
      userCode: data.userCode,
      date : data.date,
      actionHistory : data.actionHistory,
      description: data.description,
      toBranch : data.toBranch,
      saleCode : data.saleCode ?? undefined,
    } 
    if(data.currencyCode == 'none'){
      model.saleCodeNavigation = {
        saleCode : data.saleCode,
        currencyCode : undefined,
        cost : undefined,
        warranty : undefined
      }
    }
    else{
      model.saleCodeNavigation = {
        saleCode : data.saleCode,
        currencyCode : data.currencyCode ?? null,
        cost : data.cost ?? null,
        warranty:  data.warranty ?? null
      }
    }
    return model
  }
}
