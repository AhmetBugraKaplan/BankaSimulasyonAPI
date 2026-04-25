import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlSmsDogrulama } from './cepten-al-sms-dogrulama';

describe('CeptenAlSmsDogrulama', () => {
  let component: CeptenAlSmsDogrulama;
  let fixture: ComponentFixture<CeptenAlSmsDogrulama>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlSmsDogrulama]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlSmsDogrulama);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
