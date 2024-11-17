import { Injectable } from '@angular/core';
import { AccessoryHistory } from '../../../Interfaces/business/Models/Accessory';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../ApiService.service';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { Sale } from '../../../Interfaces/business/Models/Sale';    
import { SaleApiService } from '../Extras/sale-api.service';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';
import { UsdApiService } from '../Usd/usd-api.service';
import server from '../../../Logic/ServerAdress';

@Injectable({
  providedIn: 'root'
})
export class AccessoryHistoryApiService  implements ApiService<AccessoryHistory, AccessoryHistory[]> {


  constructor(public http: HttpClient,
    private saleApi : SaleApiService,
    private notificationService: NotificationSystemService,
    private authService: AuthService,
    private usdService: UsdApiService
  ) {
    authService.loadUser();
  }

  select(): Observable<AccessoryHistory[]>{
    return this.http.get<AccessoryHistory[]>(server() + '/api/AccessoryHistories');
  }
  get(id:any) : Observable<AccessoryHistory[]>{
      return this.http.get<AccessoryHistory[]>(server() + '/api/AccessoryHistories/'+id);
  }
  add(  data : any){
    let req : AccessoryHistory = this.mapperAdd(data);
    let sale : Sale | undefined;
    if(data.sale == true){
      sale = req.saleCodeNavigation
    }
    else{
      if(req.actionHistory == "Salida" && (data.currencyCode != undefined && data.currencyCode != 'none' || data.currencyCode == undefined)){
        sale = this.usdService.getSale(data.quantity, data.salePrice, 0);
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
  
          this.http.post<AccessoryHistory>(server() + '/api/AccessoryHistories/', req).subscribe(
            res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
          );
        }
      )
    }
    else{
      req.saleCode = undefined;
      req.saleCodeNavigation = undefined;
      this.http.post<AccessoryHistory>(server() + '/api/AccessoryHistories/', req).subscribe(
        res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
        err =>this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
      );
    }
  }
  edit(data : any, id : any){
    const model : any = this.mapperEdit(data);
    const ids = [data.accessoryCode, data.date];
    if(data.sale == true){
      if(model.saleCode != null){
        this.saleApi.edit(model.saleCodeNavigation, model.saleCode)
        .subscribe(res => {
          this.http.put<any>(server() + '/api/AccessoryHistories/' 
            +ids[0] + '%2C' + ids[1]+'?code=' +ids[0] +"&date=" + ids[1], model).subscribe(
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
              this.http.put<any>(server() + '/api/AccessoryHistories/' 
                +ids[0] + '%2C' + ids[1]+'?code=' +ids[0] +"&date=" + ids[1], model).subscribe(
                  res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
                  err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
          })
        }
        else{
          this.http.put<any>(server() + '/api/AccessoryHistories/' 
            +ids[0] + '%2C' + ids[1]+'?code=' +ids[0] +"&date=" + ids[1], model).subscribe(
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
            this.http.put<any>(server() + '/api/AccessoryHistories/' 
              +ids[0] + '%2C' + ids[1]+'?code=' +ids[0] +"&date=" + ids[1], model).subscribe(
                res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
                err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
              }
            ,err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
          )
      }
      else
      {
        sale = undefined;
        this.http.put<any>(server() + '/api/AccessoryHistories/' 
          +ids[0] + '%2C' + ids[1]+'?code=' +ids[0] +"&date=" + ids[1], model).subscribe(
            res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
        
      }
    }
  }
  delete(data : any){
    const ids = [data.accessoryCode, data.date];
    return this.http.delete<any>(server() + '/api/AccessoryHistories/'
                      +ids[0] + '%2C' + ids[1]+'?code=' +ids[0] +"&date=" + ids[1]);
  }
  mapperAdd(data:any): AccessoryHistory{
    if(data.actionHistory != "Transferido desde otra sucursal" && data.actionHistory != "Transferido hacia otra sucursal"){
      data.toBranch = this.authService.myUser.value.branch;
    }

    let model : AccessoryHistory = {
      accessoryCode : data.accessoryCode,
      userCode: this.authService.myUser.value.userCode!,
      date : data.date,
      actionHistory : data.actionHistory,
      description: data.description,
      supplierCode: data.supplierCode == 'none'? null: data.supplierCode,
      toBranch : data.toBranch,
      quantity:   data.quantity,
      saleCode : "default",
    };
    console.log(data, model)
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
    
    const model: AccessoryHistory= {
      accessoryCode : data.accessoryCode,
      userCode: data.userCode,
      date : data.date,
      actionHistory : data.actionHistory,
      description: data.description,
      supplierCode: data.supplierCode == 'none'? null: data.supplierCode,
      toBranch : data.toBranch,
      quantity:   data.quantity,
      saleCode : data.saleCode ?? undefined,
    }
    if(data .currencyCode == 'none'){
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
