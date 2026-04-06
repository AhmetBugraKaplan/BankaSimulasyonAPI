import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParaTransfer } from './para-transfer';

describe('ParaTransfer', () => {
  let component: ParaTransfer;
  let fixture: ComponentFixture<ParaTransfer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParaTransfer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParaTransfer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
