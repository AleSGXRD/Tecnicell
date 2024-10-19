import { TestBed } from '@angular/core/testing';

import { ScreenApiService } from './screen-api.service';

describe('ScreenApiService', () => {
  let service: ScreenApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScreenApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
