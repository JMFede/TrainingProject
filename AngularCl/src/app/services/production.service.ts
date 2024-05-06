import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from '../environment/environment';


@Injectable({
  providedIn: 'root'
})
export class ProductionService {
  orderUpdated = new Subject<number>();
  private hubConnection?: HubConnection;

  constructor() { }

  startConnection() {
    console.log('Starting SignalR connection', `${environment.API_URL}planthub`);
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.API_URL}planthub`).withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.error('Error while starting SignalR connection: ', err));

    this.hubConnection.on('OrderUpdated', (orderId: number) => {
      console.log('OrderUpdated event received', orderId);
      this.orderUpdated.next(orderId);
    });
  }

  stopConnection(){
    this.hubConnection?.stop().catch(error => console.error(error));
  }
}
