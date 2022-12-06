import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { CategoryDto } from 'src/app/models/Categories/CategoryDto';
import { TransactionDto } from 'src/app/models/Transactions/TransactionDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit {
  
  id: string | undefined = undefined;
  amount: number | undefined = undefined;
  type: number = 0;
  categoryId: string | undefined = undefined;
  accountId: string | undefined = undefined;
  date: Date | undefined = undefined;
  description: string | undefined = undefined;
  
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
    this.id = transaction.id;
    this.amount = transaction.amount;
    this.type = transaction.type;
    this.categoryId = transaction.category?.id;
    this.accountId = transaction.account!.id;
    let date = new Date(transaction.date);
    this.date = date;
    this.description = transaction.description;
    
    this.modalService.open(content, {
      ariaLabelledBy: 'modal-basic-title',
      centered: true
    });
  }
  
  updateTransactions() {
      this.transactionsService.updateTransaction({
        transactionId: this.id!,
        amount: this.amount!,
        type: this.type,
        categoryId: this.categoryId!,
        accountId: this.accountId!,
        date: this.date!,
        description: this.description!
      }).subscribe(() => {
        this.transactionsService.fetchTransactions();
        
        this.transactionsService.incomeTransactionsUpdated.emit();
        this.transactionsService.expensesTransactionsUpdated.emit();
        this.modalService.dismissAll();
      }
    );
  }
}
