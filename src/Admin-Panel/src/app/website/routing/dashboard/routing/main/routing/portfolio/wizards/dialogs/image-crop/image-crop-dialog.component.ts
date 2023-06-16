import { Component, Output, EventEmitter, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { ImageCroppedEvent } from 'ngx-image-cropper';

@Component({
  selector: 'app-dashboard-admin-new',
  templateUrl: 'image-crop-dialog.component.html',
  styleUrls: ['image-crop-dialog.component.css']
})
export class ImageCropDialogComponent {
  @Output() onSubmit = new EventEmitter();

  croppedImage: any = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data,
    private sanitizer: DomSanitizer
  ) { }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = this.sanitizer.bypassSecurityTrustUrl(event.objectUrl);
  }
}