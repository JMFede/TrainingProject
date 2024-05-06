import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.API_URL;
  private USERNAME_KEY = 'username';
  isLog = false;

  constructor(private httpClient: HttpClient, private router: Router) { }

  setUsername(username: string){
    sessionStorage.setItem(this.USERNAME_KEY, username);
    this.isLoggedIn();
    console.log("setUsername", username, "log", this.isLog);
  }
  getUsername() {
    return sessionStorage.getItem(this.USERNAME_KEY);
  }
  clearUsername() {
    sessionStorage.removeItem(this.USERNAME_KEY);
    this.isLoggedIn();
  }
  isLoggedIn(){
    this.isLog = (this.getUsername !== null);
    return this.isLog;
  }
  redirect(){
    if (this.isLog) {
      console.log("redirect", this.isLog);
      this.router.navigate(['page1']);
    }
    else {
      console.log("redirect", this.isLog);
      this.router.navigate(['login']);
    }
  }

  isRegistered(username: string): Observable<boolean> {
    console.log("login", username);
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.get<boolean>(this.apiUrl + 'api/IsRegistered'+`?username=${username}`, { headers: header });
  }

}
