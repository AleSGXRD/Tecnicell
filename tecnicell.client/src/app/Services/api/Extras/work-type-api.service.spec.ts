import { TestBed } from '@angular/core/testing';

import { WorkTypeApiService } from './work-type-api.service';

describe('WorkTypeApiService', () => {
  let service: WorkTypeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkTypeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
