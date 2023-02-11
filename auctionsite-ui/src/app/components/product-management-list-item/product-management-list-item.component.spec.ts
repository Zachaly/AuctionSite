import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductManagementListItemComponent } from './product-management-list-item.component';

describe('ProductManagementListItemComponent', () => {
  let component: ProductManagementListItemComponent;
  let fixture: ComponentFixture<ProductManagementListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductManagementListItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductManagementListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
