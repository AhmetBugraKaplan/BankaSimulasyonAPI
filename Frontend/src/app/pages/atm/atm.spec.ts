import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Atm } from './atm';

describe('Atm', () => {
  let component: Atm;
  let fixture: ComponentFixture<Atm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Atm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Atm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
