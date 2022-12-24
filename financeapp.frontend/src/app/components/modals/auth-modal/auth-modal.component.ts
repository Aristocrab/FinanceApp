import { Component } from '@angular/core';
import { UserDto } from 'src/app/models/Users/UserDto';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-auth-modal',
  templateUrl: './auth-modal.component.html',
  styleUrls: ['./auth-modal.component.css']
})
export class AuthModalComponent {
  
    loginUser: UserDto = {
      username: '',
      password: ''
    }
  
    registerUser: UserDto = {
      username: '',
      password: ''
    }
    
    constructor(private userService: UsersService) { }
    
    demoLogin() {
      this.userService.login({
        username: 'user',
        password: 'pass'
      });
    }
    
    login() {
      this.userService.login(this.loginUser);
    }
    
    register() {
      this.userService.register(this.registerUser);
    }
}
