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
  
  selectedTransaction: UpdateTransactionDto = {
    transactionId: Guid.createEmpty().toString(),
    description: '',
    accountId: Guid.createEmpty().toString(),
    categoryId: Guid.createEmpty().toString(),
    amount: 0,
    type: 0,
    date: new Date().toISOString().split('T')[0]
  };
  @Input() transactions: TransactionDto[] = [];
  
  constructor(public categoriesService: CategoriesService,
    public accountsService: AccountsService,
    public transactionsService: TransactionsService,
    private modalService: NgbModal) {
  }
  
  ngOnInit(): void {
    console.log(this.transactions);
  }
  
  selectTransaction(transaction: TransactionDto, content: any) {
    this.selectedTransaction.transactionId = transaction.id;
    this.selectedTransaction.amount = transaction.amount;
    this.selectedTransaction.type = transaction.type;
    this.selectedTransaction.categoryId = transaction.category?.id ?? Guid.create().toString();
    this.selectedTransaction.accountId = transaction.account!.id;
    this.selectedTransaction.date = transaction.date;
    this.selectedTransaction.description = transaction.description;
    
    this.modalService.open(content, {
      ariaLabelledBy: 'modal-basic-title',
      centered: true
    });
  }
  
}
