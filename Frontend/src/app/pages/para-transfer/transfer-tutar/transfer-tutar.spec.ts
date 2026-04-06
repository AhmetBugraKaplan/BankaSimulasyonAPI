import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferTutar } from './transfer-tutar';

describe('TransferTutar', () => {
  let component: TransferTutar;
  let fixture: ComponentFixture<TransferTutar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransferTutar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransferTutar);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
