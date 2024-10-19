import { TestBed } from '@angular/core/testing';

import { UsdApiService } from './usd-api.service';

describe('UsdApiService', () => {
  let service: UsdApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsdApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
