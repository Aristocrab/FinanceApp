import { Component, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guid } from 'guid-typescript';
import { TransactionDto } from 'src/app/models/Transactions/TransactionDto';
import { UpdateTransactionDto } from 'src/app/models/Transactions/UpdateTransactionDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-update-transaction-modal',
  templateUrl: './update-transaction-modal.component.html',
  styleUrls: ['./update-transaction-modal.component.css']
})
export class UpdateTransactionModalComponent {
  @Input() updateTransactionDto: UpdateTransactionDto = {
    transactionId: Guid.createEmpty().toString(),
    description: '',
    accountId: Guid.createEmpty().toString(),
    categoryId: Guid.createEmpty().toString(),
    amount: 0,
    type: 0,
    date: new Date().toISOString().split('T')[0]
  };
  
  constructor(private transactionsService: TransactionsService,
    private modalService: NgbModal,
    public categoriesService: CategoriesService,
    public accountsService: AccountsService
    ) {
  }
  
  close() {
    this.modalService.dismissAll();
  }
  
  updateTransactions() {
    this.transactionsService.updateTransaction(this.updateTransactionDto!)
    .subscribe(() => {
      
      this.transactionsService.incomeTransactionsUpdated.emit();
      this.transactionsService.expensesTransactionsUpdated.emit();
      this.modalService.dismissAll();
    }
  );
}
}
