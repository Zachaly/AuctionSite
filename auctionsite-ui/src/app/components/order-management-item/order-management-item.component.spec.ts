import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderManagementItemComponent } from './order-management-item.component';

describe('OrderManagementItemComponent', () => {
  let component: OrderManagementItemComponent;
  let fixture: ComponentFixture<OrderManagementItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderManagementItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderManagementItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
