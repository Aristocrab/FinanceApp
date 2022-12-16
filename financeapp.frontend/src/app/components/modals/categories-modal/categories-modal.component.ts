import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoriesService } from 'src/app/services/categories.service';

@Component({
  selector: 'app-categories-modal',
  templateUrl: './categories-modal.component.html',
  styleUrls: ['./categories-modal.component.css']
})
export class CategoriesModalComponent implements OnInit {
  
  categoryName: string | undefined;
  
  constructor(private modalService: NgbModal, public categoriesService: CategoriesService) 
  { }
  
  ngOnInit(): void {  
  }
  
  close() {
    this.modalService.dismissAll();
  }
  
  keyDown(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.createCategory();
    }
  }
  
  spanSwitchCategory(e: HTMLElement, categoryId: string) {
    let txt = e.innerText;
    let input = document.createElement('input');
    input.value = txt;
    input.onblur = () => {
      e.innerText = input.value;
      this.updateCategory(categoryId, input.value);
    }
    input.onkeydown = (event) => {
      if (event.key === 'Enter') {
        input.blur();
      }
    }
    e.innerText = '';
    e.appendChild(input);
    input.focus();
  }
  
  updateCategory(categoryId: string, name: string) {
    this.categoriesService.updateCategory({
      categoryId: categoryId,
      name: name
    }).subscribe(() => {
      this.categoriesService.categoriesUpdated.emit();
      this.categoryName = '';
    });
  }
  
  createCategory() {
    this.categoriesService.createCategory({
      name: this.categoryName!
    }).subscribe(() => {
      this.categoriesService.categoriesUpdated.emit();
      this.categoryName = '';
    });
  }
  
  deleteCategory(categoryId: string) {
    this.categoriesService.deleteCategory({
      categoryId: categoryId
    }).subscribe(() => {
      this.categoriesService.categoriesUpdated.emit();
    });
  }
}
