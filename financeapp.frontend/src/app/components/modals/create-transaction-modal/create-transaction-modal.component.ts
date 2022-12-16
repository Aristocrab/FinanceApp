import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateTransactionDto } from 'src/app/models/Transactions/CreateTransactionDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-create-transaction-modal',
  templateUrl: './create-transaction-modal.component.html',
  styleUrls: ['./create-transaction-modal.component.css']
})
export class CreateTransactionModalComponent {
  createTransactionDto: CreateTransactionDto = {
    description: '',
    accountId: '',
    categoryId: '',
    amount: 1,
    type: 0,
    date: new Date().toISOString().split('T')[0]
  }; 
  
	constructor(private modalService: NgbModal, 
    public categoriesService: CategoriesService, 
    public accountsService: AccountsService,
    private transactionsService: TransactionsService) {}
  
  
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
      
      this.createTransactionDto = {
        description: '',
        accountId: '',
        categoryId: '',
        amount: 1,
        type: 0,
        date: new Date().toISOString().split('T')[0]
      };
    });
  }
  
  close() {
    this.modalService.dismissAll();
  }
}
