import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { CategoryDto } from 'src/app/models/Categories/CategoryDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-new-transaction',
  templateUrl: './new-transaction.component.html',
  styleUrls: ['./new-transaction.component.css']
})
export class NewTransactionComponent implements OnInit {
  amount: number | undefined = undefined;
  type: number = 0;
  categoryId: string | undefined = undefined;
  accountId: string | undefined = undefined;
  date: Date | undefined = undefined;
  description: string | undefined = undefined;
  
  categories: CategoryDto[] | undefined;
  accounts: AccountDto[] | undefined;
  
	constructor(private modalService: NgbModal, 
    private categoriesService: CategoriesService, 
    private accountsService: AccountsService,
    private transactionsService: TransactionsService) {}
  
  ngOnInit(): void {
    this.categoriesService.getCategories().subscribe(result => {
      this.categories = result;
    });
    
    this.accountsService.getAccounts().subscribe(result => {
      this.accounts = result;
    });
    
    this.categoriesService.categoriesUpdated.subscribe(() => {
      this.categoriesService.getCategories().subscribe(result => {
        this.categories = result;
      });
    });
  }
  
  createTransaction() {
    this.transactionsService.createTransaction({
      amount: this.amount!,
      type: this.type,
      categoryId: this.categoryId!,
      accountId: this.accountId!,
      date: this.date!,
      description: this.description!
    }).subscribe(() => {
      this.modalService.dismissAll();
      this.transactionsService.fetchTransactions();
      
      if(this.type == 0) {
        this.transactionsService.expensesTransactionsUpdated.emit();
      } else if(this.type == 1) {
        this.transactionsService.incomeTransactionsUpdated.emit();
      }
    });
  }

  open(content: any) {
		this.modalService.open(content, { 
      ariaLabelledBy: 'modal-basic-title',
      centered: true 
    });
	}
}
