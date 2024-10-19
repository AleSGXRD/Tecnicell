import { TestBed } from '@angular/core/testing';

import { BatteryHistoryApiService } from './battery-history-api.service';

describe('BatteryHistoryApiService', () => {
  let service: BatteryHistoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BatteryHistoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
