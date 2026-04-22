import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlCepnoGiris } from './cepten-al-cepno-giris';

describe('CeptenAlCepnoGiris', () => {
  let component: CeptenAlCepnoGiris;
  let fixture: ComponentFixture<CeptenAlCepnoGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlCepnoGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlCepnoGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
