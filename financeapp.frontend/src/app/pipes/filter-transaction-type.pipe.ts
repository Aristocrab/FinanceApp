import { Pipe, PipeTransform } from '@angular/core';
import { TransactionDto } from '../models/TransactionDto';
import { TransactionType } from '../models/TransactionType';

@Pipe({
  name: 'filterTransactionType'
})
export class FilterTransactionTypePipe implements PipeTransform {

  transform(transactions: TransactionDto[], transactionType: number): TransactionDto[] {
    if (!transactions) return [];
    
    return transactions.filter(x => {
      return x.type === transactionType;
    });
  }

}
