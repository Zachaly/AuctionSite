import { TestBed } from '@angular/core/testing';

import { ListStockService } from './list-stock.service';

describe('ListStockService', () => {
  let service: ListStockService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListStockService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
