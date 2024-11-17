import { TestBed } from '@angular/core/testing';

import { DiaryWorkApiService } from './diary-work-api.service';

describe('DiaryWorkApiService', () => {
  let service: DiaryWorkApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DiaryWorkApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
