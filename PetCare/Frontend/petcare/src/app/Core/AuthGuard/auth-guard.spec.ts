import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { authguard} from './auth-guard';

describe('authguard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authguard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
