import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HavaleTipi } from './havale-tipi';

describe('HavaleTipi', () => {
  let component: HavaleTipi;
  let fixture: ComponentFixture<HavaleTipi>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HavaleTipi]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HavaleTipi);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
