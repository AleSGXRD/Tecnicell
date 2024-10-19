import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ApiService } from '../ApiService.service';
import { reloadPage } from '../../../Logic/ReloadPage';
import { NotificationSystemService } from '../../notification-system.service';
import { Brand } from '../../../Interfaces/business/Models/Brand';

@Injectable({
  providedIn: 'root'
})
export class BrandApiService implements ApiService<Brand,Brand> {

  constructor(public http: HttpClient,
    private notificationService: NotificationSystemService
  ) {}

  select(): Observable<Brand[]>{
    return this.http.get<Brand[]>(environment.url + '/api/Brands');
  }
  get(id:any) : Observable<Brand>{
      return this.http.get<Brand>(environment.url + '/api/Brands/'+id);
  }
  add(data : any){
    this.http.post(environment.url + '/api/Brands/',data).subscribe(
      res=> this.notificationService.showNotifcation("Se ha añadido el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar añadir el elemento.", 1)
    )
  }
  edit(data : any){
    this.http.put(environment.url + '/api/Brands/' + data.name,data).subscribe(
      res=> this.notificationService.showNotifcation("Se ha editado el elemento con exito!", 0),
      err => this.notificationService.showNotifcation("Ha ocurrido un error al intentar editar el elemento.", 1)
    )
  }
  delete(data : any){
      return this.http.delete(environment.url + '/api/Brands/'+data.name);
  }
}
