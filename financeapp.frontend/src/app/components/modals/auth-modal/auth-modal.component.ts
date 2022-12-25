import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { catchError, of, timeout } from 'rxjs';
import { UserDto } from 'src/app/models/Users/UserDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-auth-modal',
  templateUrl: './auth-modal.component.html',
  styleUrls: ['./auth-modal.component.css']
})
export class AuthModalComponent {
  
    error: string | undefined;
  
    loginUser: UserDto = {
      username: '',
      password: ''
    }
  
    registerUser: UserDto = {
      username: '',
      password: ''
    }
    
    constructor(private userService: UsersService) { }
    
    buttonClicked(form: NgForm, event: any) {
      if(!form.form.valid) {
        form.form.markAllAsTouched();
        event.preventDefault();
      }
    }
    
    getErrorMessage(err: any): string {
      console.log(JSON.parse(err.error));
      switch(err.status) {
        case 400:
          return JSON.parse(err.error)[0].ErrorMessage;
        case 403:
        case 404:
          return JSON.parse(err.error)["error"];
      }
      return "";
    }
    
    catchApiError(err: any) {
      let errorMessage = this.getErrorMessage(err);
      this.error = errorMessage;
      setTimeout(() => {
        this.error = undefined;
      }, 3000);
      
      return of(errorMessage);
    }
    
    demoLogin() {
      this.userService.login({
        username: 'user',
        password: 'pass'
      }).subscribe();
    }
    
    login() {
      this.userService.login(this.loginUser)
      .pipe(catchError((err) => this.catchApiError(err)))
      .subscribe();
    }
    
    register() {
      this.userService.register(this.registerUser)
      .pipe(catchError((err) => this.catchApiError(err)))
      .subscribe();
    }
}
