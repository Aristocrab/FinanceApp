import { Pipe, PipeTransform } from '@angular/core';
import { AccountDto } from '../models/Accounts/AccountDto';
import { TransactionDto } from '../models/Transactions/TransactionDto';

@Pipe({
  name: 'filterTransactionsByAccount'
})
export class FilterTransactionsByAccountPipe implements PipeTransform {

  transform(transactions: TransactionDto[], account: AccountDto | undefined): TransactionDto[] {
    if (!transactions) return [];

    if (!account) return transactions;

    return transactions.filter(x => {
      return x.account!.id === account.id;
    });
  }

}
