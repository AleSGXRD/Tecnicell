import { TestBed } from '@angular/core/testing';

import { AccessoryApiRequestService } from './accessory-api-request.service';

describe('AccessoryApiRequestService', () => {
  let service: AccessoryApiRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccessoryApiRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
