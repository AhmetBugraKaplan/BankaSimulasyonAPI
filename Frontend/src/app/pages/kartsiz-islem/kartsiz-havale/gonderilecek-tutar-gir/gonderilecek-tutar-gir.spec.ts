import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GonderilecekTutarGir } from './gonderilecek-tutar-gir';

describe('GonderilecekTutarGir', () => {
  let component: GonderilecekTutarGir;
  let fixture: ComponentFixture<GonderilecekTutarGir>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GonderilecekTutarGir]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GonderilecekTutarGir);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
