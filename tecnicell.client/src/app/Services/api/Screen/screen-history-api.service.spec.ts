import { TestBed } from '@angular/core/testing';

import { ScreenHistoryApiService } from './screen-history-api.service';

describe('ScreenHistoryApiService', () => {
  let service: ScreenHistoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScreenHistoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
