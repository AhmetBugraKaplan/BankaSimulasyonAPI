import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelNoGir } from './tel-no-gir';

describe('TelNoGir', () => {
  let component: TelNoGir;
  let fixture: ComponentFixture<TelNoGir>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelNoGir]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TelNoGir);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
