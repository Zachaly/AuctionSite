import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ListStockService } from 'src/app/services/list-stock.service';
import { ListService } from 'src/app/services/list.service';
import ListListItemModel from 'src/models/ListListItemModel';
import AddListStockRequest from 'src/models/request/AddListStockRequest';

@Component({
  selector: 'app-add-stock-to-list',
  templateUrl: './add-stock-to-list.component.html',
  styleUrls: ['./add-stock-to-list.component.css']
})
export class AddStockToListComponent implements OnInit {
  @Input() stockId: number = 0
  @Input() quantity: number = 0
  @Output() exit: EventEmitter<any> = new EventEmitter()
  listId: number = 0

  lists: ListListItemModel[] = []

  constructor(private authService: AuthService, private listService: ListService,
    private listStockService: ListStockService) { }

  ngOnInit(): void {
    this.listService.getUserLists(this.authService.userData.userId)
      .subscribe(res => this.lists = res.data!)
  }

  addToList() {
    const request: AddListStockRequest = {
      listId: this.listId,
      stockId: this.stockId,
      quantity: this.quantity
    }

    this.listStockService.postListStock(request)
      .subscribe(() => this.exit.emit())
  }
  cancel(){
    this.exit.emit()
  }
}
