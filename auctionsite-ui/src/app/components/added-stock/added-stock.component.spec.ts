import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddedStockComponent } from './added-stock.component';

describe('AddedStockComponent', () => {
  let component: AddedStockComponent;
  let fixture: ComponentFixture<AddedStockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddedStockComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AddedStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
