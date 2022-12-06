import { TransactionType } from "./TransactionType";

export interface UpdateTransactionDto {
    transactionId: string;
    amount: number;
    description: string;
    categoryId: string;
    accountId: string;
    type: TransactionType;
    date: Date;        
}
