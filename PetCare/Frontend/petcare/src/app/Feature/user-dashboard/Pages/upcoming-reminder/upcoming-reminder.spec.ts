import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpcomingReminder } from './upcoming-reminder';

describe('UpcomingReminder', () => {
  let component: UpcomingReminder;
  let fixture: ComponentFixture<UpcomingReminder>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpcomingReminder]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpcomingReminder);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
