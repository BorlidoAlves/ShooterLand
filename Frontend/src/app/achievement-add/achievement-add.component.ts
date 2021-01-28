import { HttpClient } from '@angular/common/http';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { Achievement } from '../models/Achievement';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-achievement-add',
  templateUrl: './achievement-add.component.html',
  styleUrls: ['./achievement-add.component.css']
})
export class AchievementAddComponent implements OnInit {

  form: FormGroup;
  success:any
  error:any;
  

  constructor(public rest: RestService,
    private route: ActivatedRoute,
    private router: Router, public http: HttpClient,public fb: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      Description: [''],
      Type:[''],
      Value:[''],
      avatar:[null]
  })
}

uploadFile(event) {
  const file = (event.target as HTMLInputElement).files[0];
  this.form.patchValue({
    avatar: file
  });
  this.form.get('avatar').updateValueAndValidity()
}

  submitForm() {
    var formData: any = new FormData();
    formData.append("Description", this.form.get('Description').value);
    formData.append("Value", this.form.get('Value').value);
    formData.append("Type",this.form.get('Type').value);
    formData.append("MedalFile", this.form.get('avatar').value);
  
    this.http.post('http://localhost:5000/Achievement/Create', formData).subscribe(
      (result:any) => {
        
        this.success="Achievement created";
        this.error="";
      },
      (err) => {
        console.log(err);
        this.error=err.error.message;
        
      }
    )
  }

  
    

  
}
