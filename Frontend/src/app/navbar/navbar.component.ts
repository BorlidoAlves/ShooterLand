import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  id: any;
  role:any;

  constructor(private router: Router,private authServive: AuthenticationService) { }

  ngOnInit(): void {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.id = currentUser.id;
    this.role=currentUser.type;
  }


  logout() {
    this.authServive.logout();
    this.router.navigate(['/login']);
  }
}
