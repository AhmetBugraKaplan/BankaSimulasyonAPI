import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AliciHesapGiris } from './alici-hesap-giris';

describe('AliciHesapGiris', () => {
  let component: AliciHesapGiris;
  let fixture: ComponentFixture<AliciHesapGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AliciHesapGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AliciHesapGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
