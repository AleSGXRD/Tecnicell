import { TestBed } from '@angular/core/testing';

import { BatteryApiService } from './battery-api.service';

describe('BatteryApiService', () => {
  let service: BatteryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BatteryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
