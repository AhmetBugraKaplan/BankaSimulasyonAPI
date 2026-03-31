import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LimitGoruntule } from './limit-goruntule';

describe('LimitGoruntule', () => {
  let component: LimitGoruntule;
  let fixture: ComponentFixture<LimitGoruntule>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LimitGoruntule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LimitGoruntule);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
