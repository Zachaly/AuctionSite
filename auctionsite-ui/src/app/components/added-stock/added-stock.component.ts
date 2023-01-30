import { Component, Input } from '@angular/core';
import AddStockRequest from 'src/models/request/AddStockRequest';

@Component({
  selector: 'app-added-stock',
  templateUrl: './added-stock.component.html',
  styleUrls: ['./added-stock.component.css']
})
export class AddedStockComponent {
  @Input() stock: AddStockRequest = { value: '', quantity: 0 }
}
