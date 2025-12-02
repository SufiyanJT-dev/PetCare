import { TestBed } from '@angular/core/testing';

import { Remainder } from './remainder';

describe('Remainder', () => {
  let service: Remainder;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Remainder);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
