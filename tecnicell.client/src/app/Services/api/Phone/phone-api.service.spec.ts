import { TestBed } from '@angular/core/testing';

import { PhoneApiService } from './phone-api.service';

describe('PhoneApiService', () => {
  let service: PhoneApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhoneApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
