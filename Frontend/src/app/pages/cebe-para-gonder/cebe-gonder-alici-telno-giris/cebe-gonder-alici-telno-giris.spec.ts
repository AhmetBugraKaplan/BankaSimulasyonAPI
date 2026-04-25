import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CebeGonderKendiTelnoGiris } from './cebe-gonder-alici-telno-giris';

describe('CebeGonderKendiTelnoGiris', () => {
  let component: CebeGonderKendiTelnoGiris;
  let fixture: ComponentFixture<CebeGonderKendiTelnoGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CebeGonderKendiTelnoGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CebeGonderKendiTelnoGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
