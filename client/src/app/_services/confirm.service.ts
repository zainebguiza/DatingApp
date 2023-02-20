import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { config, map, Observable } from 'rxjs';
import { ConfirmDialogComponent } from '../modals/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {
  bsMoadalRef?:BsModalRef<ConfirmDialogComponent>;

  constructor(private modalService:BsModalService) { }
  confirm(
    title='Confirmation',
    message='Are you sure you want to do this?',
    btnOKText='Ok',
    btnCancelText='Cancel'
  ):Observable<boolean>
  {
    const config={
      initialState:{
        title,
        message,
        btnOKText,
        btnCancelText
      }
    }
      this.bsMoadalRef=this.modalService.show(ConfirmDialogComponent,config);
      return this.bsMoadalRef.onHidden!.pipe(
        map(()=>{
          return this.bsMoadalRef!.content!.result
        })
      )
  }
}
