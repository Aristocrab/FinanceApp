import { TransactionType } from "./TransactionType";

export interface TransactionsStatsDto {
    timePeriod: string;
    amount: number;
    type: TransactionType;
}
