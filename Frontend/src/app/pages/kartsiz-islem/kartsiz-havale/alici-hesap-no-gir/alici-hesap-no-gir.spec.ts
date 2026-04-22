import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AliciHesapNoGir } from './alici-hesap-no-gir';

describe('AliciHesapNoGir', () => {
  let component: AliciHesapNoGir;
  let fixture: ComponentFixture<AliciHesapNoGir>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AliciHesapNoGir]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AliciHesapNoGir);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
