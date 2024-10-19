import { TestBed } from '@angular/core/testing';

import { AccessoryHistoryApiService } from './accessory-history-api.service';

describe('AccessoryHistoryApiService', () => {
  let service: AccessoryHistoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccessoryHistoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
