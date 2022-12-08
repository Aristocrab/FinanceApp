import { Guid } from "guid-typescript";

export interface CategoryDto {
    id: string;
    name: string;     
    transactionsCount: number;
}
