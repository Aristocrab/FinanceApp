import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { AccountDto } from '../models/Accounts/AccountDto';
import { CategoryDto } from '../models/Categories/CategoryDto';
import { CategoryStatsDto } from '../models/Categories/CategoryStatsDto';
import { CreateCategoryDto } from '../models/Categories/CreateCategoryDto';
import { DeleteCategoryDto } from '../models/Categories/DeleteCategoryDto';
import { UpdateCategoryDto } from '../models/Categories/UpdateCategoryDto';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService extends ApiService {
  
  categories: CategoryDto[] | undefined;
  categoriesUpdated = new EventEmitter();
  
  constructor(private http: HttpClient) {
    super();
  }
  
  fetchCategories() {
    this.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }
  
  getCategories() {
    return this.http.get<CategoryDto[]>(`${ApiService.baseUrl}/Categories`);
  }
  
  getCategoryStats(type: number, selectedAccount: AccountDto | undefined) {
    if(selectedAccount) {
      return this.http.get<CategoryStatsDto[]>(`${ApiService.baseUrl}/Categories/Stats/${type}/${selectedAccount.id}`);
    } else {
      return this.http.get<CategoryStatsDto[]>(`${ApiService.baseUrl}/Categories/Stats/${type}`);
    }
  }
  
  createCategory(category: CreateCategoryDto) {
    return this.http.post(`${ApiService.baseUrl}/Categories/new`, category);
  }
  
  updateCategory(category: UpdateCategoryDto) {
    return this.http.put(`${ApiService.baseUrl}/Categories/update`, category);
  }
  
  deleteCategory(category: DeleteCategoryDto) {
    return this.http.delete(`${ApiService.baseUrl}/Categories/delete`, 
     {
        body: category
     });
  }
}
