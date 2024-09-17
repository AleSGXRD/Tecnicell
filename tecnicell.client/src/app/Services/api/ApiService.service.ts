import { HttpClient } from "@angular/common/http";
import { Injector, Injectable } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { firstValueFrom, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
export class ApiService<T>{

    constructor(public http: HttpClient) {}

    select() : Observable<T[]>{
        return this.http.get<T[]>('');
    }
    get(id:any) : Observable<T>{
        return this.http.get<T>(''+id);
    }
    add(data : any){
        return this.http.get('');
    }
    edit(data : any, id : any){
        return this.http.get('');
    }
    delete(id : any){
        return this.http.get('');
    }

}