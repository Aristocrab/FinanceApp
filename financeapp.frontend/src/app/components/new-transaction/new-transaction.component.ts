import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guid } from 'guid-typescript';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { CategoryDto } from 'src/app/models/Categories/CategoryDto';
import { CreateTransactionDto } from 'src/app/models/Transactions/CreateTransactionDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-new-transaction',
  templateUrl: './new-transaction.component.html',
  styleUrls: ['./new-transaction.component.css']
})
export class NewTransactionComponent implements OnInit {
  createTransactionDto: CreateTransactionDto = {
    description: '',
    accountId: '',
    categoryId: '',
    amount: 1,
    type: 0,
    date: new Date().toISOString().split('T')[0]
  }; 
  
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
  
  buttonClicked(form: NgForm, event: any) {
    if(!form.form.valid) {
      form.form.markAllAsTouched();
      event.preventDefault();
    }
  }
    
  createTransaction() {
    this.transactionsService.createTransaction(this.createTransactionDto!).subscribe(() => {
      this.modalService.dismissAll();
      this.transactionsService.fetchTransactions();
      
      if(this.createTransactionDto.type == 0) {
        this.transactionsService.expensesTransactionsUpdated.emit();
      } else if(this.createTransactionDto.type == 1) {
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
