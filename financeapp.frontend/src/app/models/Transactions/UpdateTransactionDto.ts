import { TransactionType } from "./TransactionType";

export interface UpdateTransactionDto {
    transactionId: string;
    categoryId: string;
    accountId: string;
    
    amount: number;
    description: string;
    type: TransactionType;
    date: string;        
}
