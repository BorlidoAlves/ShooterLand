import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../models/User';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() userData: User = new User();
  @Input() repeatedPassword:any
  error:any

  constructor(  public rest: RestService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (localStorage.getItem('currentUser')) {
      this.router.navigate(['/homepage']);
    }
  }

  addUser() {
    if(this.userData.Password!==this.repeatedPassword){
     this.error="Passwords don´t match";
      
    }
    else{
      this.rest.addUser(this.userData).subscribe(
        (result:any) => {
          console.log(result);
          this.router.navigate(['/login']);
        },
        (err) => {
          console.log(err);
          this.error=err.error.message;
          
        }
      );
    }
    
  }
}
