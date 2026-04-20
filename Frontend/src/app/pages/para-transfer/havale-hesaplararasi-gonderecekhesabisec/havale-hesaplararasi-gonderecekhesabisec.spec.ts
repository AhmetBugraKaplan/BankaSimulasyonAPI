import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HavaleHesaplararasiGonderecekhesabisec } from './havale-hesaplararasi-gonderecekhesabisec';

describe('HavaleHesaplararasiGonderecekhesabisec', () => {
  let component: HavaleHesaplararasiGonderecekhesabisec;
  let fixture: ComponentFixture<HavaleHesaplararasiGonderecekhesabisec>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HavaleHesaplararasiGonderecekhesabisec]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HavaleHesaplararasiGonderecekhesabisec);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
