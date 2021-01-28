import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-edit-password',
  templateUrl: './edit-password.component.html',
  styleUrls: ['./edit-password.component.css']
})
export class EditPasswordComponent implements OnInit {

  @Input() userData: any = { password: '', repPassword: '' };
  error: any;
  success: any;
  id:any;


  constructor(public rest: RestService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    if (JSON.parse(localStorage.getItem('currentUser')).id != this.id) {
      this.router.navigate(['/homepage']);
    }
    
    }
  

  changePassword() {
    this.error="";
    if (this.userData.password !== this.userData.repPassword) {
      this.error = 'Passwords don´t match';
    } else {
  
      let password: string = null;
      password = this.userData.password ;
      this.rest.changePassword(this.id, password).subscribe(
        (result) => {
          this.success = 'Password changed successfully';
          this.error="";
        },
        (err) => {
          console.log(err);
          this.error = err.error.message;
        }
      );
      
    }

  }

  cancel(){
    this.router.navigate(["/profile/"+this.id]);
  }

}
