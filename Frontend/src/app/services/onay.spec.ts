import { TestBed } from '@angular/core/testing';

import { Onay } from './onay';

describe('Onay', () => {
  let service: Onay;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Onay);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
