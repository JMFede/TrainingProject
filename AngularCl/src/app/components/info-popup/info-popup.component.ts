import {Component, Inject, ViewChild} from '@angular/core';
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
import { MatIconModule } from '@angular/material/icon';

import {provideNativeDateAdapter} from '@angular/material/core';

import { Page1Component } from '../page1/page1.component';

@Component({
  selector: 'app-info-popup',
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
    MatIconModule
  ],
  templateUrl: './info-popup.component.html',
  styleUrl: './info-popup.component.scss'
})
export class InfoPopupComponent {
  constructor(
    public dialogRef: MatDialogRef<Page1Component>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

  onCloseClick(): void {
    this.dialogRef.close(); // Close the dialog without emitting any data
  }

}
