import { TransactionType } from "./TransactionType";

export interface TransferTransactionDto {
    amount: number;
    description: string;
    accountFromId: string;
    accountToId: string;
    date: Date;        
}
