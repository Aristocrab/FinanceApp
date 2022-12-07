import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guid } from 'guid-typescript';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { CategoryDto } from 'src/app/models/Categories/CategoryDto';
import { TransactionDto } from 'src/app/models/Transactions/TransactionDto';
import { UpdateTransactionDto } from 'src/app/models/Transactions/UpdateTransactionDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit {
  
  updateTransactionDto: UpdateTransactionDto = {
    transactionId: Guid.createEmpty().toString(),
    description: '',
    accountId: Guid.createEmpty().toString(),
    categoryId: Guid.createEmpty().toString(),
    amount: 0,
    type: 0,
    date: new Date().toISOString().split('T')[0]
  };
  
  categories: CategoryDto[] | undefined;
  accounts: AccountDto[] | undefined;
  
  @Input() transactions: TransactionDto[] = [];
  
  constructor(private transactionsService: TransactionsService,
    private modalService: NgbModal,
    private categoriesService: CategoriesService,
    private accountsService: AccountsService) {
  }
  
  ngOnInit(): void {
    this.transactionsService.fetchTransactions();
    
    this.categoriesService.getCategories().subscribe(result => {
      this.categories = result;
    });
    
    this.accountsService.getAccounts().subscribe(result => {
      this.accounts = result;
    });
    
    this.categoriesService.categoriesUpdated.subscribe(() => {
      this.transactionsService.fetchTransactions();
    });
  }
  
  selectTransaction(transaction: TransactionDto, content: any) {
    this.updateTransactionDto.amount = transaction.amount;
    this.updateTransactionDto.type = transaction.type;
    this.updateTransactionDto.categoryId = transaction.category!.id;
    this.updateTransactionDto.accountId = transaction.account!.id;
    this.updateTransactionDto.date = transaction.date.toISOString().split('T')[0];
    this.updateTransactionDto.description = transaction.description;
    
    this.modalService.open(content, {
      ariaLabelledBy: 'modal-basic-title',
      centered: true
    });
  }
  
  updateTransactions() {
      this.transactionsService.updateTransaction(this.updateTransactionDto)
      .subscribe(() => {
        this.transactionsService.fetchTransactions();
        
        this.transactionsService.incomeTransactionsUpdated.emit();
        this.transactionsService.expensesTransactionsUpdated.emit();
        this.modalService.dismissAll();
      }
    );
  }
}
