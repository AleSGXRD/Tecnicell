import { TestBed } from '@angular/core/testing';
import { BatteryBrandApiService } from './battery-brand-api.service';

describe('BatteryBrandApiService', () => {
  let service: BatteryBrandApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BatteryBrandApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
