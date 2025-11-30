import { TestBed } from '@angular/core/testing';

import { WeightHistoryService } from './weight-history-service';

describe('WeightHistoryService', () => {
  let service: WeightHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeightHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
