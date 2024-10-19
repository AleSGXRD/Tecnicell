import { TestBed } from '@angular/core/testing';

import { PhoneHistoryApiService } from './phone-history-api.service';

describe('PhoneHistoryApiService', () => {
  let service: PhoneHistoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhoneHistoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
