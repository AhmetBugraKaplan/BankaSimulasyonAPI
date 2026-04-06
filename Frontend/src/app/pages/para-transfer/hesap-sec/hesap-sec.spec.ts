import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HesapSec } from './hesap-sec';

describe('HesapSec', () => {
  let component: HesapSec;
  let fixture: ComponentFixture<HesapSec>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HesapSec]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HesapSec);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
