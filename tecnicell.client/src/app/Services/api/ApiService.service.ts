import { HttpClient } from "@angular/common/http";
import { Injector, Injectable } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { firstValueFrom, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
export class ApiService<VIEW, RESPONSE>{

    constructor(public http: HttpClient) {}

    select() : Observable<VIEW[]>{
        return this.http.get<VIEW[]>('');
    }
    get(id:any) : Observable<RESPONSE>{
        return this.http.get<RESPONSE>(''+id);
    }
    add(data : any){
        
    }
    edit(data : any, id : any){
    }
    delete(data : any){
        return this.http.get('');
    }

}