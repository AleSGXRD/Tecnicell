import { TestBed } from '@angular/core/testing';

import { NotificationSystemService } from './notification-system.service';

describe('NotificationSystemService', () => {
  let service: NotificationSystemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotificationSystemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
