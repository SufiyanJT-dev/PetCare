import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMedicalEventDialog } from './add-medical-event-dialog';

describe('AddMedicalEventDialog', () => {
  let component: AddMedicalEventDialog;
  let fixture: ComponentFixture<AddMedicalEventDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddMedicalEventDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMedicalEventDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
