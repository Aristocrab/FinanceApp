import { Pipe, PipeTransform } from '@angular/core';
import { TransactionDto } from '../models/TransactionDto';

@Pipe({
  name: 'searchTransaction'
})
export class SearchTransactionPipe implements PipeTransform {

  transform(transactions: TransactionDto[], searchText: string | undefined): TransactionDto[] {
    if (!transactions) return [];
    if (!searchText || searchText === undefined || searchText === '') return transactions;
    
    searchText = searchText.toLowerCase();
    return transactions.filter(x => {
      return x.description.toLowerCase().includes(searchText!.toLowerCase());
    });
  }

}
