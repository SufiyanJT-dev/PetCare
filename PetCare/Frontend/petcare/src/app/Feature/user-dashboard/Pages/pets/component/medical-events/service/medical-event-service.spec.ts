import { TestBed } from '@angular/core/testing';

import { MedicalEventService } from './medical-event-service';

describe('MedicalEventService', () => {
  let service: MedicalEventService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicalEventService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
