import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IslemOnaylandi } from './islem-onaylandi';

describe('IslemOnaylandi', () => {
  let component: IslemOnaylandi;
  let fixture: ComponentFixture<IslemOnaylandi>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IslemOnaylandi]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IslemOnaylandi);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
