import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData } from 'chart.js';

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
  
  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: true,
        text: 'By categories',
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
  
  public pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: [ 'Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange' ],
    datasets: [ {
      data: [ 300, 500, 100, 40, 120, 300 ],
    } ]
  };
  
  constructor() { }

  ngOnInit(): void {
  }

}
