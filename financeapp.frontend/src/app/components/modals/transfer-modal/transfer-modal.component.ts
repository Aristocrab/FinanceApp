import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guid } from 'guid-typescript';
import { TransferTransactionDto } from 'src/app/models/Transactions/TransferTransactionDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-transfer-modal',
  templateUrl: './transfer-modal.component.html',
  styleUrls: ['./transfer-modal.component.css']
})
export class TransferModalComponent {
  constructor(public transactionsService: TransactionsService, 
    public accountsService: AccountsService,
    public modalService: NgbModal) { }
  
  transferTransactionDto: TransferTransactionDto = {
    amount: 0,
    accountFromId: Guid.createEmpty().toString(),
    accountToId: Guid.createEmpty().toString(),
    date: new Date().toISOString().split('T')[0],
    description: ''
  }
  
  transferTransaction() {
    this.transactionsService.transferTransaction(this.transferTransactionDto).subscribe(() => {
      this.modalService.dismissAll();
      this.transactionsService.fetchTransactions();
      
      this.transactionsService.incomeTransactionsUpdated.emit();
      this.transactionsService.expensesTransactionsUpdated.emit();
    });
  }
  
  close() {
    this.modalService.dismissAll();
  }
}
