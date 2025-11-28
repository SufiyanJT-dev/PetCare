import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPetDialog } from './add-pet-dialog';

describe('AddPetDialog', () => {
  let component: AddPetDialog;
  let fixture: ComponentFixture<AddPetDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddPetDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPetDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
