import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParaCek } from './para-cek';

describe('ParaCek', () => {
  let component: ParaCek;
  let fixture: ComponentFixture<ParaCek>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParaCek]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParaCek);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
