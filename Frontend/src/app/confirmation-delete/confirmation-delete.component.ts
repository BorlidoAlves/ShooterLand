import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { RestService } from '../rest.service';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-confirmation-delete',
  templateUrl: './confirmation-delete.component.html',
  styleUrls: ['./confirmation-delete.component.css']
})
export class ConfirmationDeleteComponent implements OnInit {

  @Input() title: string;
  @Input() message: string;
  @Input() btnOkText: string;
  @Input() btnCancelText: string;

  id:any;
  currentUser:any;

  constructor(private activeModal: NgbActiveModal,
    public rest: RestService,
    private route: ActivatedRoute,
    private router: Router,  private authServive: AuthenticationService) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.id = this.currentUser.id;
  }

  public decline() {
    this.activeModal.close(false);
  }

  public accept() {
    this.activeModal.close(true);
  }

  public dismiss() {
    this.activeModal.dismiss();
  }

  deleteUser(){
    console.log("ID"+this.id);
    
    this.rest.deleteUser(this.id).subscribe(
      (res) => {
          this.activeModal.close(true);
          this.authServive.logout();
          this.router.navigate(['/login']);
        
        console.log('Utilizador apagado');
      },
      (err) => {
        console.log(err);
      }
    );
  }


}
