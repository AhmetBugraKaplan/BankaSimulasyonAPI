import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmsOnayKoduGir } from './sms-onay-kodu-gir';

describe('SmsOnayKoduGir', () => {
  let component: SmsOnayKoduGir;
  let fixture: ComponentFixture<SmsOnayKoduGir>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SmsOnayKoduGir]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SmsOnayKoduGir);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
