import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SifreDegistir } from './sifre-degistir';

describe('SifreDegistir', () => {
  let component: SifreDegistir;
  let fixture: ComponentFixture<SifreDegistir>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SifreDegistir]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SifreDegistir);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
