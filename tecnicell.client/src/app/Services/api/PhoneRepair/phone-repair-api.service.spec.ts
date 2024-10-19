import { TestBed } from '@angular/core/testing';

import { PhoneRepairApiService } from './phone-repair-api.service';

describe('PhoneRepairApiService', () => {
  let service: PhoneRepairApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhoneRepairApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
