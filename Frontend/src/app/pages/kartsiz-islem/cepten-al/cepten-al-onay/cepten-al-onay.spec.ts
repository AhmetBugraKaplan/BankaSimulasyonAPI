import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlOnay } from './cepten-al-onay';

describe('CeptenAlOnay', () => {
  let component: CeptenAlOnay;
  let fixture: ComponentFixture<CeptenAlOnay>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlOnay]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlOnay);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
