import { Component, Input, OnInit } from '@angular/core';
import { tick } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  @Input() email: String 
  success:any
  error:any
 

  constructor( public rest: RestService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
   
  }

  reset()
  {
    console.log("THE EMAIL IS : ",this.email);
    this.rest.resetPassword(this.email).subscribe(
      (result: any) => {
        console.log('Email Sent');
        this.success = 'Your new password was sent to your email';
        this.error="";
      },
      (err) => {
        console.log('ERROR:', err);
        this.error=err.error.message
      }
    );
  }
  
}
