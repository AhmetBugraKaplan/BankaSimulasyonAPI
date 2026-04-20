import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HavaleHesaplararasiGonderilecekhesabisec } from './havale-hesaplararasi-gonderilecekhesabisec';

describe('HavaleHesaplararasiGonderilecekhesabisec', () => {
  let component: HavaleHesaplararasiGonderilecekhesabisec;
  let fixture: ComponentFixture<HavaleHesaplararasiGonderilecekhesabisec>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HavaleHesaplararasiGonderilecekhesabisec]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HavaleHesaplararasiGonderilecekhesabisec);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
