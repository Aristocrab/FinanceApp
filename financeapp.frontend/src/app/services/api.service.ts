import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  protected static readonly baseUrl = 'http://aristocrab.me/api';
  public static readonly BackendUrl = 'aristocrab.me';
  
}
