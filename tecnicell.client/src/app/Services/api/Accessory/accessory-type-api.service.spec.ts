import { TestBed } from '@angular/core/testing';

import { AccessoryTypeApiService } from './accessory-type-api.service';

describe('AccessoryTypeApiService', () => {
  let service: AccessoryTypeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccessoryTypeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
