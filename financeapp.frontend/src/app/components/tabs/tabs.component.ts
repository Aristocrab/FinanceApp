import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TransactionDto } from 'src/app/models/TransactionDto';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.css']
})
export class TabsComponent implements OnInit {

  active = 1;
  searchText: string | undefined = undefined;
  transactions: TransactionDto[] = [];
  
  constructor(private http: HttpClient) { 
  }
  
  ngOnInit(): void {
    this.http.get<TransactionDto[]>('http://localhost:5238/api/Transactions').subscribe(result => {
      this.transactions = result;
    });
  }

}
