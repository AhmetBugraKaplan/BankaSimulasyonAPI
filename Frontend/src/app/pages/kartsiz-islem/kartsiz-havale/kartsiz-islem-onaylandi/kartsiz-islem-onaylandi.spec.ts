import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KartsizIslemOnaylandi } from './kartsiz-islem-onaylandi';

describe('KartsizIslemOnaylandi', () => {
  let component: KartsizIslemOnaylandi;
  let fixture: ComponentFixture<KartsizIslemOnaylandi>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KartsizIslemOnaylandi]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KartsizIslemOnaylandi);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
