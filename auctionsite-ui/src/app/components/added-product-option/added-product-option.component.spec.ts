import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddedProductOptionComponent } from './added-product-option.component';

describe('AddedProductOptionComponent', () => {
  let component: AddedProductOptionComponent;
  let fixture: ComponentFixture<AddedProductOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddedProductOptionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddedProductOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
