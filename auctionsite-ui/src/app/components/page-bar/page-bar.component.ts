import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-page-bar',
  templateUrl: './page-bar.component.html',
  styleUrls: ['./page-bar.component.css']
})
export class PageBarComponent {
  @Input() count: number = 0
  @Output() changePage: EventEmitter<number> = new EventEmitter()

  currentIndex = 0

  changeIndex(index: number){
    this.currentIndex = index
    this.changePage.emit(index)
  }
}
