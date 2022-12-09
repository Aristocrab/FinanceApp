import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData } from 'chart.js';
import { TransactionType } from 'src/app/models/Transactions/TransactionType';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {

  public expensesChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: true,
        text: 'Expenses',
      },
    },
  };
  
  public incomeChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: true,
        text: 'Income',
      },
    },
  };
  
  public lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: false,
      },
    },
    elements: {
      bar: {
        backgroundColor: '#86c7f3',
        borderColor: '#86c7f3',
        borderWidth: 2
      }
    }
  };
  
  selectedTimePeriod: number = 0;
  
  public expensesData: ChartData<'pie', number[], string | string[]> | undefined = undefined;
  public incomeData: ChartData<'pie', number[], string | string[]> | undefined = undefined;
  public lineData: ChartData<'line', number[], string | string[]> | undefined = undefined;
  
  constructor(private categoriesService: CategoriesService, 
    private transactionsService: TransactionsService,
    private accountsService: AccountsService
    ) { }
    
  timePeriodChange(value: number): void {
    this.selectedTimePeriod = value;
    this.updateLineChartOptions();
  }
  
  updateExpensesChartOptions(): void {
    this.categoriesService.getCategoryStats(0, this.accountsService.selectedAccount).subscribe(result => {
      this.expensesData = {
        labels: result.map(r => r.category.name),
        datasets: [
          {
            data: result.map(r => r.count),
          },
        ],
      };
    });
  }
  
  updateIncomeChartOptions(): void {
    this.categoriesService.getCategoryStats(1, this.accountsService.selectedAccount).subscribe(result => {
      this.incomeData = {
        labels: result.map(r => r.category.name),
        datasets: [
          {
            data: result.map(r => r.count),
          },
        ],
      };
    });
  }
   
  updateLineChartOptions(): void {
    this.transactionsService.getTransactionsStats(this.selectedTimePeriod).subscribe(result => {
      let labels = result.map(r => r.timePeriod);
      labels.sort()
      this.lineData = {
        labels: [...new Set(labels)],
        datasets: [
          {
            data: result.filter(r => r.type == TransactionType.Expense).map(r => r.amount),
            borderColor: '#e53b44',
            pointBackgroundColor: '#9e2835'
          },
          {
            data: result.filter(r => r.type == TransactionType.Income).map(r => r.amount),
            borderColor: '#63c64d',
            pointBackgroundColor: '#327345'
          },
        ],
      };
    });
  }

  updateData(): void {
    this.updateExpensesChartOptions();
    this.updateIncomeChartOptions();
    this.updateLineChartOptions();
  }
  
  ngOnInit(): void {    
    this.updateData();
    
    this.transactionsService.incomeTransactionsUpdated.subscribe(() => {
      this.updateIncomeChartOptions();
      this.updateLineChartOptions();
    });
    
    this.transactionsService.expensesTransactionsUpdated.subscribe(() => {
      this.updateExpensesChartOptions();
      this.updateLineChartOptions();
    });
    
    this.accountsService.selectedAccountUpdated.subscribe(() => {
      this.updateData();
    });
  }
}
