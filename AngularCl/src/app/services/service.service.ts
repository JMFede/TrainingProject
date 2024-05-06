import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  private path = environment.API_URL; //set the path to the api

  constructor(private httpClient: HttpClient) { }

  getAllOrderData(): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.get(this.path + 'api/GetOrder', { headers: header });
  }
  getLinesData(): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.get(this.path + 'api/GetLine', { headers: header });
  }
  getTypesData(): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.get(this.path + 'api/GetType', { headers: header });
  }
  getUsersData(): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.get(this.path + 'api/GetUser', { headers: header });
  }
  getStatusData(): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.get(this.path + 'api/GetStatus', { headers: header });
  }
  addOrder(data: any): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.path + 'api/AddOrder', data, { headers: header });
  }
  addUser(data: any): Observable<any> {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.path + 'api/AddUser', data, { headers: header });
  }
  updateOrderData(data: any): Observable<any>{
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.put(this.path + 'api/EditOrder', data, {headers: header});
  }
}
