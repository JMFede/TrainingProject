import { Component } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule, MatFormFieldControl} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatButtonModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule,
    RouterModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

    constructor(private authService: AuthService, private router: Router) { }

    isRegistered: boolean = true;

    onSubmit(loginForm: NgForm) {
      const username = loginForm.value.username;

      this.authService.isRegistered(username)
      .subscribe((res) => {
        console.log(res);

        if (res) {
          this.isRegistered = true;
          this.authService.setUsername(username);
        }
        else {
          this.isRegistered = false;
        }
        this.authService.redirect();
      });
      
    }
    toRegister(){
      this.router.navigate(['register']);
    }
    
}
