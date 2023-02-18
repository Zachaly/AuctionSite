import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import Category from 'src/models/Category';

@Component({
  selector: 'app-category-select',
  templateUrl: './category-select.component.html',
  styleUrls: ['./category-select.component.css']
})
export class CategorySelectComponent implements OnInit {
  categories: Category[] = []
  curentCategoryId: number = 0

  @Output() selectCategory: EventEmitter<number> = new EventEmitter()

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategories()
      .subscribe(res => this.categories = res.data!)
  }

  onSelect(){
    this.selectCategory.emit(this.curentCategoryId)
  }
}
