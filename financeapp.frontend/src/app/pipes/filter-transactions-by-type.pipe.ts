import { Pipe, PipeTransform } from '@angular/core';
import { TransactionDto } from '../models/Transactions/TransactionDto';

@Pipe({
  name: 'filterTransactionsByType'
})
export class FilterTransactionsByTypePipe implements PipeTransform {

  transform(transactions: TransactionDto[], transactionType: number): TransactionDto[] {
    if (!transactions) return [];
    
    if (transactionType === -1) return transactions;
    
    return transactions.filter(x => {
      return x.type === transactionType;
    });
  }

}
