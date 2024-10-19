import { TestBed } from '@angular/core/testing';

import { ActionHistoryApiService } from './action-history-api.service';

describe('ActionHistoryApiService', () => {
  let service: ActionHistoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActionHistoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
