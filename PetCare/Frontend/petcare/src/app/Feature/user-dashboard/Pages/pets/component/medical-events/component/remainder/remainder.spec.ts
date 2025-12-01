import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Remainder } from './remainder';

describe('Remainder', () => {
  let component: Remainder;
  let fixture: ComponentFixture<Remainder>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Remainder]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Remainder);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
