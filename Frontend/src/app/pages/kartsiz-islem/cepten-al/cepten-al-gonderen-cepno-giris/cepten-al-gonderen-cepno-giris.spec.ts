import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlGonderenCepnoGiris } from './cepten-al-gonderen-cepno-giris';

describe('CeptenAlGonderenCepnoGiris', () => {
  let component: CeptenAlGonderenCepnoGiris;
  let fixture: ComponentFixture<CeptenAlGonderenCepnoGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlGonderenCepnoGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlGonderenCepnoGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
