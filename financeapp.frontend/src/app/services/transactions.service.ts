import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { CreateTransactionDto } from '../models/Transactions/CreateTransactionDto';
import { DeleteTransactionDto } from '../models/Transactions/DeleteTransactionDto';
import { TransactionDto } from '../models/Transactions/TransactionDto';
import { TransactionsStatsDto } from '../models/Transactions/TransactionsStatsDto';
import { TransferTransactionDto } from '../models/Transactions/TransferTransactionDto';
import { UpdateTransactionDto } from '../models/Transactions/UpdateTransactionDto';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService extends ApiService {

  public transactions: TransactionDto[] = [];
  
  incomeTransactionsUpdated = new EventEmitter();
  expensesTransactionsUpdated = new EventEmitter();
  
  constructor(private http: HttpClient) {
    super();
  }
  
  getTransactionsStats(type: number) {
    return this.http.get<TransactionsStatsDto[]>(`${ApiService.baseUrl}/Transactions/stats/${type}`);
  }
  
  fetchTransactions() {
    return this.http.get<TransactionDto[]>(`${ApiService.baseUrl}/Transactions`)
    .subscribe(result => {
      this.transactions = result;
    });
  }
  
  createTransaction(transaction: CreateTransactionDto) {
    return this.http.post(`${ApiService.baseUrl}/Transactions/new`, transaction);
  }
  
  updateTransaction(transaction: UpdateTransactionDto) {
    return this.http.put(`${ApiService.baseUrl}/Transactions/update`, transaction);
  }
  
  transferTransaction(transaction: TransferTransactionDto) {
    return this.http.post(`${ApiService.baseUrl}/Transactions/transfer`, transaction);
  }
  
  deleteTransaction(deleteTransactionDto: DeleteTransactionDto) {
    return this.http.delete(`${ApiService.baseUrl}/Transactions/delete`, {
      body: deleteTransactionDto
    });
  }
}
