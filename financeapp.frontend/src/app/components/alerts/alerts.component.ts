import { Component, OnInit } from '@angular/core';
import { Alert, AlertsService } from 'src/app/services/alerts.service';

@Component({
  selector: 'app-alerts',
  templateUrl: './alerts.component.html',
  styleUrls: ['./alerts.component.css']
})
export class AlertsComponent implements OnInit {
  constructor(
    protected alertsService: AlertsService
  ) { }
  
  ngOnInit(): void {
    this.alertsService.alertsUpdated.subscribe(() => {
    });
  }
  
  close(alert: Alert) {
    this.alertsService.alerts = this.alertsService.alerts.filter(a => a !== alert);
  }
  
}
