import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlTcGiris } from './cepten-al-tc-giris';

describe('CeptenAlTcGiris', () => {
  let component: CeptenAlTcGiris;
  let fixture: ComponentFixture<CeptenAlTcGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlTcGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlTcGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
