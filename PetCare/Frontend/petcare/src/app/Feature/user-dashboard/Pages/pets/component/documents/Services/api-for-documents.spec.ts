import { TestBed } from '@angular/core/testing';

import { ApiForDocuments } from './api-for-documents';

describe('ApiForDocuments', () => {
  let service: ApiForDocuments;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiForDocuments);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
