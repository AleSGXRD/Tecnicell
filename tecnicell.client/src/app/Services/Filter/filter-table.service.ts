import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, filter } from 'rxjs';
import { FilterField } from '../../Interfaces/tools/Filters/Filters';
import { SearchsApiService } from '../api/Extras/searchs-api.service';

@Injectable({
  providedIn: 'root'
})
export class FilterTableService {

  public filters : BehaviorSubject<any> = new BehaviorSubject(null);
  public values : BehaviorSubject<any> = new BehaviorSubject([]);

  public nameToSave : string = '';
  private searchsApi : SearchsApiService = inject(SearchsApiService);

  constructor() { }

  emit(values : any){
    this.filters.next(values);

    values.filter((value:FilterField) => {
      if(value.save == true){
        this.searchsApi.add({value: value.value})
      }
    })
  }
}
