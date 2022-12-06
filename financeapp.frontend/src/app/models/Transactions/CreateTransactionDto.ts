import { TransactionType } from "./TransactionType";

export interface CreateTransactionDto {
    amount: number;
    description: string;
    categoryId: string;
    accountId: string;
    type: TransactionType;
    date: Date;        
}
