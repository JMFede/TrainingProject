import {Component, EventEmitter, Inject, Output, ViewChild} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
  MatDialogModule,
} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatDividerModule} from '@angular/material/divider';
import {MatSelectModule} from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

import {provideNativeDateAdapter} from '@angular/material/core';
import { Observable, Subject, of } from 'rxjs';
import { debounceTime, switchMap, catchError } from 'rxjs/operators';

import { Page1Component } from '../page1/page1.component';
import { ServiceService } from '../../services/service.service';

@Component({
  selector: 'app-add-order',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatDialogModule,
    MatDatepickerModule,
    MatDividerModule,
    MatIconModule,
    MatSelectModule,
    Page1Component,
  ],
  templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.scss'
})
export class AddOrderComponent {
  constructor(public dialogRef: MatDialogRef<Page1Component>, @Inject(MAT_DIALOG_DATA) public data: any) {  }

  @Output() callComponentFunction = new EventEmitter<void>();

  minDate = new Date();

  onCancelClick(): void {
    this.dialogRef.close(); // Close the dialog without emitting any data
  }

  onOkClick(): void {
    this.callComponentFunction.emit();
    // Emit the data to be passed back to the parent component
      this.dialogRef.close({
      data: this.data
    });
  }


}
