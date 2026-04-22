import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlTutarbilgisiGiris } from './cepten-al-tutarbilgisi-giris';

describe('CeptenAlTutarbilgisiGiris', () => {
  let component: CeptenAlTutarbilgisiGiris;
  let fixture: ComponentFixture<CeptenAlTutarbilgisiGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlTutarbilgisiGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlTutarbilgisiGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
