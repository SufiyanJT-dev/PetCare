import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalEvents } from './medical-events';

describe('MedicalEvents', () => {
  let component: MedicalEvents;
  let fixture: ComponentFixture<MedicalEvents>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalEvents]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalEvents);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
