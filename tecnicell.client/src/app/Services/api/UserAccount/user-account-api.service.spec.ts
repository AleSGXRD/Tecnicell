import { TestBed } from '@angular/core/testing';

import { UserAccountApiService } from './user-account-api.service';

describe('UserAccountApiService', () => {
  let service: UserAccountApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserAccountApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
