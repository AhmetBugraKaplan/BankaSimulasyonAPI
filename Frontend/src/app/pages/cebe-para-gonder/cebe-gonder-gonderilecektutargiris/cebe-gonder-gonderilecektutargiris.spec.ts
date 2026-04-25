import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CebeGonderGonderilecektutargiris } from './cebe-gonder-gonderilecektutargiris';

describe('CebeGonderGonderilecektutargiris', () => {
  let component: CebeGonderGonderilecektutargiris;
  let fixture: ComponentFixture<CebeGonderGonderilecektutargiris>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CebeGonderGonderilecektutargiris]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CebeGonderGonderilecektutargiris);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
