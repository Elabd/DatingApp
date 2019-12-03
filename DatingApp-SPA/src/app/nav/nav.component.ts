import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) { }
  ngOnInit() {
  }
  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log("logged in successfully")
    }, error => {
      console.log(error);
    });
  }
  loggedIn() {
    var token = localStorage.getItem('token');
    return !!token;
  }
  logout() {
    localStorage.removeItem('token');
    console.log('logged ouy');
  }
}
