import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  protected static readonly baseUrl = 'http://localhost:5238/api';
  
}