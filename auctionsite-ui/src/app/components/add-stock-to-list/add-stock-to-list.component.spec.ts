import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStockToListComponent } from './add-stock-to-list.component';

describe('AddStockToListComponent', () => {
  let component: AddStockToListComponent;
  let fixture: ComponentFixture<AddStockToListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddStockToListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddStockToListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
