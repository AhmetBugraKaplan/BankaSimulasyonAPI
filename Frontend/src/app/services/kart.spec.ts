import { TestBed } from '@angular/core/testing';

import { Kart } from './kart';

describe('Kart', () => {
  let service: Kart;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Kart);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
