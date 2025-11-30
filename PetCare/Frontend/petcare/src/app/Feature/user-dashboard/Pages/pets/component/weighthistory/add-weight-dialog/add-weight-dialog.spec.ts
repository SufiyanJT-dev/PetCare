import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWeightDialog } from './add-weight-dialog';

describe('AddWeightDialog', () => {
  let component: AddWeightDialog;
  let fixture: ComponentFixture<AddWeightDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddWeightDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddWeightDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
