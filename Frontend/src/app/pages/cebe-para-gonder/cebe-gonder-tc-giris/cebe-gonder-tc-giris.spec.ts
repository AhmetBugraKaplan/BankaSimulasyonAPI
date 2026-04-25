import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CebeGonderTcGiris } from './cebe-gonder-tc-giris';

describe('CebeGonderTcGiris', () => {
  let component: CebeGonderTcGiris;
  let fixture: ComponentFixture<CebeGonderTcGiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CebeGonderTcGiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CebeGonderTcGiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
