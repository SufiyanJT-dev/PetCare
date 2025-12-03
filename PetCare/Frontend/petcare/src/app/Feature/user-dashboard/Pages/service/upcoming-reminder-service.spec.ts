import { TestBed } from '@angular/core/testing';

import { UpcomingReminderService } from './upcoming-reminder-service';

describe('UpcomingReminderService', () => {
  let service: UpcomingReminderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpcomingReminderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
