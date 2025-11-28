import { TestBed } from '@angular/core/testing';

import { GetIdServices } from './get-id-services';

describe('GetIdServices', () => {
  let service: GetIdServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetIdServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
