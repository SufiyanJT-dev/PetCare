import { TestBed } from '@angular/core/testing';

import { authServices } from './auth-services';

describe('authServices', () => {
  let service: authServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(authServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
