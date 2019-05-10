import { Component, ViewEncapsulation, Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';

/**
 * Component for user confirmation dialogs
 * @class
 */
@Injectable()
@Component({
  selector: 'confirm-component',
  templateUrl: './confirm.component.html'
})
export class ConfirmComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public dialogData: any) {
  }
}
