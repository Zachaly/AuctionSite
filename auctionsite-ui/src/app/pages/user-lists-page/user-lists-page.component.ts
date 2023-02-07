import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ListService } from 'src/app/services/list.service';
import ListListItemModel from 'src/models/ListListItemModel';
import AddListRequest from 'src/models/request/AddListRequest';

@Component({
  selector: 'app-user-lists-page',
  templateUrl: './user-lists-page.component.html',
  styleUrls: ['./user-lists-page.component.css']
})
export class UserListsPageComponent implements OnInit {
  lists: ListListItemModel[] = []
  newListName: string = ''

  constructor(private authService: AuthService, private listService: ListService) { }

  ngOnInit(): void {
    if (this.authService.userData.userId) {
      this.loadLists()
    } else {
      this.authService.userSubject.subscribe(() => this.loadLists())
    }
  }

  addList() {
    const request: AddListRequest = {
      userId: this.authService.userData.userId,
      name: this.newListName
    }

    this.listService.postList(request).subscribe(() => {
      this.newListName = ''
      this.loadLists()
    })
  }

  loadLists() {
    this.listService.getUserLists(this.authService.userData.userId).subscribe(res => this.lists = res.data!)
  }

  deleteList(id: number){
    this.listService.deleteList(id).subscribe(() => this.loadLists())
  }
}
