import { Guid } from "guid-typescript";
import { TransactionType } from "./TransactionType";

export interface CreateTransactionDto {
    categoryId: string;
    accountId: string;
    
    amount: number;
    description: string;
    type: TransactionType;
    date: string;        
}
