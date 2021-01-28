import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestService } from '../rest.service';
import { AuthenticationService } from '../authentication.service';
import { ConfirmationDeleteService } from '../confirmation-delete.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  id:any;
  isSelf:any
  userInfo:any;
  role:any
  currentUser:any
  isAdmin:any

  constructor(public rest: RestService,
    private route: ActivatedRoute,
    private router: Router,  
    private authServive: AuthenticationService,
    private ConfirmationDeleteService: ConfirmationDeleteService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.getInfo();

    this.currentUser=JSON.parse(localStorage.getItem('currentUser'));
    if (this.currentUser.id != this.id) {
      
      this.isSelf=false;
    }
    else{
      this.isSelf=true;
    

    }
    this.role=this.currentUser.type;
    console.log("role:"+this.role);
    if(this.role=="Admin"){
      this.isAdmin=true;
    }
    else{
      this.isAdmin=false;
    }

    console.log("IS ADMIN?:"+this.isAdmin)
    
  }

  getInfo() {
    this.rest.userInfo(this.id).subscribe((data:{}) => {
      this.userInfo = data;
      console.log("INFORMAÇAO:"+this.userInfo);
    });
  }

  openConfirmationDelete(){
    this.ConfirmationDeleteService.confirm('Delete account', 'Are you sure you want to delete your account ?')
    .then((confirmed) => console.log('User confirmed:', confirmed))
    .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }
  
  changePage(){
    console.log("CLICK: "+this.id);
    var link="/change-password/"+this.id;
    console.log("LINK: "+link)
    this.router.navigate(["/change-password",this.id]);
  }
}
