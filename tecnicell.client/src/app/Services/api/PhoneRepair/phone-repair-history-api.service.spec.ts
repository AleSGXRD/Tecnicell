import { TestBed } from '@angular/core/testing';

import { PhoneRepairHistoryApiService } from './phone-repair-history-api.service';

describe('PhoneRepairHistoryApiService', () => {
  let service: PhoneRepairHistoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhoneRepairHistoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
