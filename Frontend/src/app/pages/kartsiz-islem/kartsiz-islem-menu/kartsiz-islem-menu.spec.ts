import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KartsizIslemMenu } from './kartsiz-islem-menu';

describe('KartsizIslemMenu', () => {
  let component: KartsizIslemMenu;
  let fixture: ComponentFixture<KartsizIslemMenu>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KartsizIslemMenu]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KartsizIslemMenu);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
