import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Weighthistory } from './weighthistory';

describe('Weighthistory', () => {
  let component: Weighthistory;
  let fixture: ComponentFixture<Weighthistory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Weighthistory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Weighthistory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
