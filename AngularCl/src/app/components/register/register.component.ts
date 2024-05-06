import { Component } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule, MatFormFieldControl} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { AuthService } from '../../auth/auth.service';
import { ServiceService } from '../../services/service.service';

@Component({
  selector: 'app-register',
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
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  constructor(private authService: AuthService, private service: ServiceService, private router: Router) { }
  formSubmitted: boolean = false;
  isRegistered: boolean = false;

  onSubmit(loginForm: NgForm): any {
    const username = loginForm.value.username;

    this.authService.isRegistered(username)
    .subscribe((res) => {
      console.log(res);

      if (res != null) {
        console.log("User already exists");
        this.formSubmitted = true;
        this.isRegistered = true;
      }
      else {
        const user = {
          usedId: 0,
          userName: username
        }
        this.service.addUser(user).subscribe((res) => {
          console.log(res);
          this.formSubmitted = true;
          this.isRegistered = false;
        });
      }
    });
  }


  toLogin(){
    this.router.navigate(['login']);
    this.formSubmitted = false;
  }
}
