import { EventEmitter, Injectable } from '@angular/core';

export interface Alert {
	type: string;
	message: string;
}

@Injectable({
  providedIn: 'root'
})
export class AlertsService {

  public alerts: Alert[] = [];
  public alertsUpdated = new EventEmitter();
  
  constructor() { }
  
  addAlert(type: 'success' | 'warning' | 'info', message: string) {
    this.alerts.push({ type, message });
    this.alertsUpdated.emit();
  }
}
