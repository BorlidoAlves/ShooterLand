import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestService } from '../rest.service';
import { ConfirmationDeleteService } from '../confirmation-delete.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent implements OnInit {
  users:any = [];
  role:string;

  constructor(public rest: RestService,private route: ActivatedRoute,private router: Router,private ConfirmationDeleteService: ConfirmationDeleteService) { }

  
  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.users = [];
    this.rest.getAllUsers().subscribe((data:{})=>{
      this.users=data;
      console.log('USERS:'+this.users);
    });
    
  }

  deleteUser(id:string){
    this.rest.deleteUser(id).subscribe(
      (res) => {
        console.log('User deleted');
        window.location.reload();
      },
      (err) => {
        console.log(err);
      }
    );
  }

}
