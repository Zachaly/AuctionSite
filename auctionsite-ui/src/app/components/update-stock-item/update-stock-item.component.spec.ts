import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateStockItemComponent } from './update-stock-item.component';

describe('UpdateStockItemComponent', () => {
  let component: UpdateStockItemComponent;
  let fixture: ComponentFixture<UpdateStockItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateStockItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateStockItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
