import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Guid } from 'guid-typescript';
import { IconName } from 'ngx-bootstrap-icons';
import { TransactionDto } from 'src/app/models/Transactions/TransactionDto';
import { TransactionType } from 'src/app/models/Transactions/TransactionType';
import { AccountsService } from 'src/app/services/accounts.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent {

  @Input() transaction!: TransactionDto;
  @Output() update = new EventEmitter<TransactionDto>();
  
  constructor(private transactionsService: TransactionsService, public accountsService: AccountsService) {
    
  }
  getIcon(accountId: string) {
    if(this.accountsService.accounts && this.accountsService.accounts!.find(x => x.id === accountId)) {
      return this.accountsService.icons[this.accountsService.accounts!.find(x => x.id === accountId)!.icon];
    } else {
      return this.accountsService.icons[0];
    }
  }
  
  updateTransaction(): void {
    this.update.emit(this.transaction);
  }
  
  deleteTransaction(id: string): void {
    if(confirm('Are you sure you want to delete this transaction?')) {
      this.transactionsService.deleteTransaction({transactionId: id})
        .subscribe(() => {
            this.transactionsService.fetchTransactions();
            
            if(this.transaction.type === TransactionType.Expense) {
              this.transactionsService.expensesTransactionsUpdated.emit();
            } else if(this.transaction.type === TransactionType.Income) {
              this.transactionsService.incomeTransactionsUpdated.emit();
            }
          }
        );
    }
  }

}
