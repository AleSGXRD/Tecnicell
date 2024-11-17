import { TestBed } from '@angular/core/testing';

import { SearchsApiService } from './searchs-api.service';

describe('SearchsApiService', () => {
  let service: SearchsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SearchsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
