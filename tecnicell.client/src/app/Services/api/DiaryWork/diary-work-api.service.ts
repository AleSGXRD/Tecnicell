import { Injectable } from '@angular/core';
import { DiaryWork } from '../../../Interfaces/business/Models/DiaryWork';
import server from '../../../Logic/ServerAdress';
import { HttpClient } from '@angular/common/http';
import { SaleApiService } from '../Extras/sale-api.service';
import { NotificationSystemService } from '../../notification-system.service';
import { AuthService } from '../Authorization/auth.service';
import { UsdApiService } from '../Usd/usd-api.service';
import { ApiService } from '../ApiService.service';
import { Observable } from 'rxjs';
import { Sale } from '../../../Interfaces/business/Models/Sale';

@Injectable({
  providedIn: 'root'
})
export class DiaryWorkApiService implements ApiService<DiaryWork, DiaryWork[]> {


  constructor(public http: HttpClient,
    private saleApi : SaleApiService,
    private notificationService: NotificationSystemService,
    private authService: AuthService,
    private usdService: UsdApiService
  ) {
    authService.loadUser();
  }

  select(): Observable<DiaryWork[]>{
    return this.http.get<DiaryWork[]>(server() + '/api/DiaryWorks');
  }
  get(id:any) : Observable<DiaryWork[]>{
      return this.http.get<DiaryWork[]>(server() + '/api/DiaryWorks/'+id);
  }
  add(data : any){
    let req : any = this.mapperAdd(data);
    let sale : Sale | undefined;
    if(data.sale == true){
      sale = req.saleCodeNavigation
    }
    else{
      sale = undefined
    }
    if(sale != undefined){
      this.saleApi.add(sale).subscribe(
        res => 
        {
          req.saleCode = res.saleCode;
          req.saleCodeNavigation = undefined;
  
          this.http.post<DiaryWork>(server() + '/api/DiaryWorks/', req).subscribe(
            res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
          );
        }
      )
    }
    else{
      req.saleCode = undefined;
      req.saleCodeNavigation = undefined;
      this.http.post<DiaryWork>(server() + '/api/DiaryWorks/', req).subscribe(
        res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
        err =>this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
      );
    }
  }
  
  edit(data : any, id : any){
    const model : any = this.mapperEdit(data);
    if(data.sale == true){
      if(model.saleCode != null){
        this.saleApi.edit(model.saleCodeNavigation, model.saleCode)
        .subscribe(res => {
          this.http.put<any>(server() + '/api/DiaryWorks/' + model.date, model).subscribe(
              res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
              err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
            },
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
          )
      }
      else{
        if(model.saleCodeNavigation.currencyCode != 'none'){
          this.saleApi.add(model.saleCodeNavigation).subscribe(
            res => {
              model.saleCode = res.saleCode;
              model.saleCodeNavigation = null;
              this.http.put<any>(server() + '/api/DiaryWorks/' + model.date, model).subscribe(
                  res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
                  err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
          })
        }
        else{
          this.http.put<any>(server() + '/api/DiaryWorks/' + model.date, model).subscribe(
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
            this.http.put<any>(server() + '/api/DiaryWorks/' + model.date, model).subscribe(
                res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
                err => this.notificationService.showNotifcationWithError("Ha ocurrido un error al intentar editar el elemento.", 1,err))
              }
            ,err => this.notificationService.showNotifcationWithError("Ha ocurrido un error al intentar editar el elemento.", 1,err)
          )
      }
      else
      {
        sale = undefined;
        this.http.put<any>(server() + '/api/DiaryWorks/' + model.date, model).subscribe(
            res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
            err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1))
        
      }
    }
  }
  delete(data : any){
    return this.http.delete<any>(server() + '/api/DiaryWorks/' + data.date);
  }
  mapperAdd(data:any){
    let model : DiaryWork = {
      date : data.date,
      workType : data.workType,
      description: data.description,
      saleCode : "default",
      userCode: this.authService.myUser.value.userCode!,
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
    let model : DiaryWork = {
      workType : data.workType,
      userCode: this.authService.myUser.value.userCode!,
      date : data.date,
      description: data.description,
      saleCode : "default",
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
