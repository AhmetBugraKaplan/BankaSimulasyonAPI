import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeptenAlParaayal } from './cepten-al-paraayal';

describe('CeptenAlParaayal', () => {
  let component: CeptenAlParaayal;
  let fixture: ComponentFixture<CeptenAlParaayal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CeptenAlParaayal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CeptenAlParaayal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
