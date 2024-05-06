import {LiveAnnouncer} from '@angular/cdk/a11y';
import {AfterViewInit, Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatSort, Sort, MatSortModule} from '@angular/material/sort';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
import {MatAccordion, MatExpansionModule} from '@angular/material/expansion';
import {MatDatepickerModule, MatDatepicker} from '@angular/material/datepicker';
import {MatSelectModule} from '@angular/material/select';

import { CommonModule } from '@angular/common';


import {ServiceService} from '../../services/service.service';
import {SettingsPopupComponent} from '../settings-popup/settings-popup.component';
import { DatePipe } from '@angular/common';
import { InfoPopupComponent } from '../info-popup/info-popup.component';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductionService } from '../../services/production.service';
import {interval, max } from 'rxjs';
import { AddOrderComponent } from '../add-order/add-order.component';
import { AuthService } from '../../auth/auth.service';
import { HttpClientModule } from '@angular/common/http';

const today = new Date();
const month = today.getMonth();
const year = today.getFullYear();

@Component({
  selector: 'app-page1',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatTableModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatSortModule,
    MatIcon,
    MatMenuModule,
    MatButtonModule,
    MatAccordion,
    MatExpansionModule,
    MatDatepickerModule,
    MatSelectModule,
    CommonModule
  ],
  templateUrl: './page1.component.html',
  styleUrl: './page1.component.scss'
})
export class Page1Component implements OnInit, OnDestroy{

  constructor(
    private _liveAnnouncer: LiveAnnouncer,
    public dialog: MatDialog,
    private service: ServiceService,
    private authService: AuthService,
    private datePipe: DatePipe,
    private productionService: ProductionService,
    ) 
    {
      this.dataSource = new MatTableDataSource();
    }

  orderData: any = [];
  dataSource: any = [];

  typeData: any = [];
  lineData: any = [];
  userData: any = [];
  statusData: any = [];

  noFilter = true;

  displayedColumns: string[] = ['actions', 'name', 'typeName', 'lineName', 'batch', 'quantity', 'plannedQuantity', 'plannedDate', 'userName', 'wbs', 'startingDate', 'statusName', 'start_process', 'pause_process'];
  filterSelection = {
                      selName: null, 
                      selType: null, //constrained
                      selLine: null,  //constrained
                      selBatch: null, 
                      selQuantity: {min: null, max: null},
                      selPlannedQuantity: {min: null, max: null},
                      selPlannedDate: {min: null, max: null},
                      selUserId: null,  //constrained
                      selWbs: null,
                      selStartingDate: {min: null, max: null},
                      selStatus: null  //constrained
                    };

  campaignOne = new FormGroup({
    start: new FormControl(new Date(year, month, 13)),
    end: new FormControl(new Date(year, month, 16)),
  });
  campaignTwo = new FormGroup({
    start: new FormControl(new Date(year, month, 13)),
    end: new FormControl(new Date(year, month, 16)),
  });

  ngOnInit() {
    this.getAllOrderData();
    console.log("AuthGuard -->", this.authService.getUsername());

    this.productionService.orderUpdated.subscribe((orderId: number) => {
      this.updateQuantity(orderId);
    });
    this.productionService.startConnection();
  }

  ngOnDestroy() {
    this.productionService.stopConnection();
  }

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatAccordion) accordion!: MatAccordion;
  @ViewChild('yearPicker') yearPicker!: MatDatepicker<any>; // Add this line


  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  /** Announce the change in sort state for assistive technology. */
  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }


  getAllOrderData(){
      this.service.getAllOrderData().subscribe({
      next: (res:any) => {
        console.log("From API: ",res);
        this.orderData = res;
        this.orderData.map((x:any) => {
          if (x.plannedDate !== null) {
            x.plannedDate = this.datePipe.transform(new Date(x.plannedDate), 'yyyy-MM-dd');
          }
          if (x.startingDate !== null) {
            x.startingDate = this.datePipe.transform(new Date(x.startingDate), 'yyyy-MM-dd HH:mm');
          }
          x.typeName  = null;
          x.lineName = null;
          x.userName = null;
          x.statusName = null;

          if (x.type != null) {
            x.typeName = x.type.typeName;
          }
          if (x.line != null) {
            x.lineName = x.line.lineName;
          }
          if (x.user != null) {
            x.userName = x.user.userName;
          }
          if (x.status != null) {
            x.statusName = x.status.statusName;
          }
        });
        //console.log('orderObj', this.orderData);
        
        console.log('Orders From API: ', this.orderData);

        this.dataSource.data = this.orderData;  
      },
      error: (err:any) => {
        console.log(err);
      }
    })
    this.service.getLinesData().subscribe({
      next: (res:any) => {
      this.lineData = res;
      this.lineData = this.lineData.filter((x:any) => x != null);
      console.log('Lines From API: ', this.lineData);
    },
    error: (err:any) => {
      console.log(err);
    }
    })
    this.service.getTypesData().subscribe({
      next: (res:any) => {
      this.typeData = res;
      this.typeData = this.typeData.filter((x:any) => x != null);
      console.log('Types From API: ', this.typeData);
    },
    error: (err:any) => {
      console.log(err);
    }
    })
    this.service.getUsersData().subscribe({
      next: (res:any) => {
      this.userData = res;
      this.userData = this.userData.filter((x:any) => x != null);
      console.log('Users From API: ', this.userData);
    },
    error: (err:any) => {
      console.log(err);
    }
    })
    this.service.getStatusData().subscribe({
      next: (res:any) => {
      this.statusData = res;
      this.statusData = this.statusData.filter((x:any) => x != null);
      console.log('Status From API: ', this.statusData);
    },
    error: (err:any) => {
      console.log(err);
    }
    })
  }

  applyFilter() {
    this.noFilter = true;

    this.dataSource.filterPredicate = (data: any) => {
      let nameCondition = true;
      let typeCondition = true;
      let lineCondition = true;
      let batchCondition = true;
      let quantityCondition = true;
      let plannedQuantityCondition = true;
      let plannedDateCondition = true;
      let userIdCondition = true;
      let wbsCondition = true;
      let startingDateCondition = true;
      let statusCondition = true;

      const startingDate = new Date(data.startingDate);
      const plannedDate = new Date(data.plannedDate);

      if (this.filterSelection.selName !== null) {
        nameCondition = data.name === this.filterSelection.selName;
      }
      if (this.filterSelection.selType !== null) {
        typeCondition = data.typeName === this.filterSelection.selType;
      }
      if (this.filterSelection.selLine !== null) {
        lineCondition = data.lineName === this.filterSelection.selLine;
      }
      if (this.filterSelection.selBatch !== null) {
        batchCondition = data.batch === this.filterSelection.selBatch;
      }
      if (this.filterSelection.selQuantity.min !== null && this.filterSelection.selQuantity.max !== null) {
        quantityCondition = data.quantity >= this.filterSelection.selQuantity.min && data.quantity <= this.filterSelection.selQuantity.max;
      }
      if (this.filterSelection.selPlannedQuantity.min !== null && this.filterSelection.selPlannedQuantity.max !== null) {
        plannedQuantityCondition = data.quantity >= this.filterSelection.selPlannedQuantity.min && data.quantity <= this.filterSelection.selPlannedQuantity.max;
      }
      if (this.filterSelection.selPlannedDate.min !== null && this.filterSelection.selPlannedDate.max !== null) {
        const maxDate = new Date(this.filterSelection.selPlannedDate.max);
        maxDate.setHours(23, 59, 59, 999);
        plannedDateCondition = plannedDate >= new Date(this.filterSelection.selPlannedDate.min) && plannedDate <= maxDate;
      }
      if (this.filterSelection.selUserId !== null) {
        userIdCondition = data.userId === this.filterSelection.selUserId;
      }
      if (this.filterSelection.selWbs !== null) {
        wbsCondition = data.wbs === this.filterSelection.selWbs;
      }
      if (this.filterSelection.selStartingDate.min !== null && this.filterSelection.selStartingDate.max !== null) {
        const maxStartingDate = new Date(this.filterSelection.selStartingDate.max);
        maxStartingDate.setHours(23, 59, 59, 999);
        startingDateCondition = startingDate.getTime() >= new Date(this.filterSelection.selStartingDate.min).getTime() && startingDate.getTime() <= maxStartingDate.getTime();
      }
      if (this.filterSelection.selStatus !== null) {
        statusCondition = data.statusName === this.filterSelection.selStatus;
      }

      return nameCondition && typeCondition && lineCondition && batchCondition && quantityCondition && plannedQuantityCondition && plannedDateCondition && userIdCondition && wbsCondition && startingDateCondition && statusCondition;
    };

    this.dataSource.filter = '' + Math.random();
    console.log('Filtering by', this.filterSelection);

  }

  clearFilters() {
    this.filterSelection.selName = null;
    this.filterSelection.selType = null;
    this.filterSelection.selLine = null;
    this.filterSelection.selBatch = null;
    this.filterSelection.selQuantity = {min: null, max: null};
    this.filterSelection.selPlannedQuantity = {min: null, max: null};
    this.filterSelection.selPlannedDate = {min: null, max: null};
    this.filterSelection.selUserId = null;
    this.filterSelection.selWbs = null;
    this.filterSelection.selStartingDate = {min: null, max: null};
    this.filterSelection.selStatus = null;
    this.dataSource.filter = '';
  }

  updateQuantity(orderId: number) {
    let order = this.dataSource.data.find((x:any) => x.orderId === orderId);
    order.quantity += 1;
  }

  addOrder() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '400px';
    dialogConfig.height = '600px';
  
    // Pass current values of date and line along with variables for new values
    dialogConfig.data = {
      name: '',
      type: '',
      types: this.typeData,
      line: '',
      lines: this.lineData,
      batch: '',
      plannedQuantity: 0,
      plannedDate: '',
      user: '',
      users: this.userData,
      wbs: '',
      isLineAvailable: this.isLineAvailable.bind(this)
    };
    console.log('lines', dialogConfig.data.lines);
  
    const dialogRef = this.dialog.open(AddOrderComponent, dialogConfig);
  
    dialogRef.afterClosed().subscribe(data => {
      console.log('The dialog was closed');
      if (data) {
        // Add new order if data is present
        data = data.data;
        data.plannedDate = this.datePipe.transform(data.plannedDate, 'yyyy-MM-dd');

        const newOrder = {
          orderId: 0,
          name: data.name,
          typeId: data.type.typeId,
          lineId: data.line.lineId,
          batch: data.batch,
          quantity: 0,
          plannedQuantity: data.plannedQuantity,
          plannedDate: data.plannedDate,
          userId: data.user.userId,
          wbs: data.wbs,
          startingDate: null,
          statusId: 3
        };
        this.service.addOrder(newOrder).subscribe({
          next: (res:any) => {
          console.log("--> Order added",res);
          this.getAllOrderData();
        },
        error: (err:any) => {
          console.log(err);
        }
        })
      }
    });
  }

  managePlanning(element: any): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.height = '500px';
  
    // Pass current values of date and line along with variables for new values
    console.log('lines', this.lineData);
    dialogConfig.data = {
      currentDate: element.plannedDate,
      currentLine: element.lineName,
      newLine: { lineId: 0, lineName: '' },
      newDate: '',
      lines: this.lineData,
      isLineAvailable: this.isLineAvailable.bind(this),
      isLate : this.isLate(element),
      backgroundColor: this.getRowColor(element)
    };
      
    const dialogRef = this.dialog.open(SettingsPopupComponent, dialogConfig);
  
    dialogRef.afterClosed().subscribe(data => {
      console.log('The dialog was closed');
      if (data) {
        // Update element with new values if data is present
        data.newDate = this.datePipe.transform(new Date(data.newDate), 'yyyy-MM-dd');
        element.plannedDate = data.newDate
        element.lineId = data.newLine.lineId
        element.lineId = data.newLine.lineId
        element.line.lineName = data.newLine.lineName
        element.lineName = data.newLine.lineName

        this.service.updateOrderData(element).subscribe({
          next: (res:any) => {
          console.log("--> Line updated",res);
        },
        error: (err:any) => {
          console.log(err);
        }
        })
      }
    });
  }

  openInfo(element: any): void{
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.height = '200spx';
  
    // Pass current values of date and line along with variables for new values
    dialogConfig.data = {
      currentDate: element.plannedDate,
      currentLine: element.lineName,
      orderName: element.name,
      orderStatus: element.statusName,
      isLate : this.isLate(element),
      backgroundColor: this.getRowColor(element)
    };
  
    const dialogRef = this.dialog.open(InfoPopupComponent, dialogConfig);
  
    dialogRef.afterClosed().subscribe(data => {
      console.log('The dialog was closed');
    });
  }

  startProcess(element: any) {
    // Start the process
    if (element.statusName === 'Paused' || element.statusName === 'Planned') {

      const newUser = {
        userId: 0,
        userName: this.authService.getUsername()
      }

      this.service.addUser(newUser).subscribe({
        next: (res:any) => {
        console.log(res, 'add user response from api');
        
        element.userId = res.userId
        element.userName = res.userName;
        element.statusId = 1; //Open
        element.statusName = 'Open';
        element.startingDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm');
        console.log('start process', element);

        this.service.updateOrderData(element).subscribe({
          next: (res:any) => {
          console.log(res, 'update response from api');
          this.service.getStatusData().subscribe({
            next: (res:any) => {
            this.statusData = res;
            this.statusData = this.statusData.filter((x:any) => x != null);
            },
            error: (err:any) => {
              console.log(err);
            }
            })
            },
          error: (err:any) => {
            console.log(err);
          }
          })

        },
        error: (err:any) => {
          console.log(err);
        }
      })
    }
  }
  pauseProcess(element: any) {
    // Pause the process
    if (element.statusName === 'Open') {
      element.statusId = 4; //Paused
      element.statusName = 'Paused';
      this.service.updateOrderData(element).subscribe({
        next: (res:any) => {
        console.log(res, 'update response from api');
        this.service.getStatusData().subscribe({
          next: (res:any) => {
          this.statusData = res;
          this.statusData = this.statusData.filter((x:any) => x != null);
        },
        error: (err:any) => {
          console.log(err);
        }
        })
      },
      error: (err:any) => {
        console.log(err);
      }
      })      
    }
  }
  isProcessOpen(element: any) {
    return element.statusId === 1;
  }
  isProcessClosed(element: any) {
    return element.statusId === 2;
  }
  isLate(element: any): boolean {
    // Disable if the planned date is before the current date
    if(element.statusId === 2) {
      return false;
    }
    else{
      const plannedDate = new Date(element.plannedDate);
      const currentDate = new Date();

      return plannedDate < currentDate;
    }
    
  }
  isLineAvailable(line: string, date: string): boolean {
    //search in the database if the line is present, return true if it is
    let isAvailable = true;
    this.dataSource.data.forEach((element: any) => {
      if (line=='' || (element.lineName === line && element.plannedDate === this.datePipe.transform(new Date(date), 'yyyy-MM-dd'))){
        isAvailable = false;
      }
    });
    return isAvailable;
  }
  getRowColor(element: any): string {

    if (this.isLate(element)) {
      return 'lightcoral';
    }
    else if (element.statusName === 'Closed') {
      return 'lightgrey';
    }
    return 'initial';
  }

}


