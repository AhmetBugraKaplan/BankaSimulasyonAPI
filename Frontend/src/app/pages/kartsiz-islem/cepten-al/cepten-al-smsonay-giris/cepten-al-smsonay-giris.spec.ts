import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlSmsonayGiris } from './cepten-al-smsonay-giris';

describe('CeptenAlSmsonayGiris', () => {
  let component: CeptenAlSmsonayGiris;
  let fixture: ComponentFixture<CeptenAlSmsonayGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlSmsonayGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlSmsonayGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
