import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ListStockService } from 'src/app/services/list-stock.service';
import { ListService } from 'src/app/services/list.service';
import ListModel from 'src/models/ListModel';

@Component({
  selector: 'app-list-page',
  templateUrl: './list-page.component.html',
  styleUrls: ['./list-page.component.css']
})
export class ListPageComponent implements OnInit {
  list: ListModel = { id: 0, items: [], name: '' }

  constructor(private route: ActivatedRoute, private listService: ListService,
    private authService: AuthService, private listStockService: ListStockService) { }

  ngOnInit(): void {
    if (this.authService.authorized) {
      this.load()
    } else {
      this.authService.authSubject.subscribe(() => this.load())
    }
  }

  load() {
    this.route.params.subscribe(param => {
      this.listService.getListById(param['id'])
        .subscribe(res => this.list = res.data!)
    })
  }

  removeStock(id: number) {
    this.listStockService.deleteListStock(id)
      .subscribe(() => this.list.items = this.list.items.filter(x => x.id !== id))
  }
}
