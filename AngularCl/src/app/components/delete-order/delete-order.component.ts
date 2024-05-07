import { Component, Inject } from '@angular/core';
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
import {MatDividerModule} from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';

import { Page1Component } from '../page1/page1.component';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-delete-order',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './delete-order.component.html',
  styleUrl: './delete-order.component.scss'
})
export class DeleteOrderComponent {
  constructor(
    public dialogRef: MatDialogRef<Page1Component>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

  onCloseClick(): void {
    this.dialogRef.close(); // Close the dialog without emitting any data
  }

  onConfirmClick(): void {
    this.dialogRef.close(this.data); // Close the dialog and emit data
  }

}
