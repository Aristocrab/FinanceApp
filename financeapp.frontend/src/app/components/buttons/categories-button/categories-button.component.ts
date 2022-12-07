import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { CategoryDto } from 'src/app/models/Categories/CategoryDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-categories-button',
  templateUrl: './categories-button.component.html',
  styleUrls: ['./categories-button.component.css']
})
export class CategoriesButtonComponent implements OnInit {

  categories: CategoryDto[] | undefined;
  
  categoryName: string | undefined;
  
  constructor(private modalService: NgbModal,
    private categoriesService: CategoriesService,
    private transactionsService: TransactionsService
    ) { }
  
  ngOnInit(): void {    
    this.categoriesService.getCategories().subscribe(result => {
      this.categories = result;
    });    
    
    this.transactionsService.expensesTransactionsUpdated.subscribe(() => {
      this.categoriesService.getCategories().subscribe(result => {
        this.categories = result;
      });
    });
    
    this.transactionsService.incomeTransactionsUpdated.subscribe(() => {
      this.categoriesService.getCategories().subscribe(result => {
        this.categories = result;
      });
    });
  }
  
  open(content: any) {
		this.modalService.open(content, { 
      ariaLabelledBy: 'modal-basic-title',
      centered: true 
    });
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
      this.categoriesService.getCategories().subscribe(result => {
        this.categories = result;
      });
      
      this.categoriesService.categoriesUpdated.emit();
    });
  }
  
  createCategory() {
    this.categoriesService.createCategory({
      name: this.categoryName!
    }).subscribe(() => {
      this.categoriesService.getCategories().subscribe(result => {
        this.categories = result;
        this.categoryName = undefined;
      });
      
      this.categoriesService.categoriesUpdated.emit();
    });
  }
  
  deleteCategory(categoryId: string) {
    this.categoriesService.deleteCategory({
      categoryId: categoryId
    }).subscribe(() => {
      this.categoriesService.getCategories().subscribe(result => {
        this.categories = result;
      });
      
      this.categoriesService.categoriesUpdated.emit();
    });
  }
}
