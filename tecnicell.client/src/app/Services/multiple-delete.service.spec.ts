import { TestBed } from '@angular/core/testing';

import { MultipleDeleteService } from './multiple-delete.service';

describe('MultipleDeleteService', () => {
  let service: MultipleDeleteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MultipleDeleteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
