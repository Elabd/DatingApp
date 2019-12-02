import { Component, OnInit, EventEmitter,Input, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService) { }
  ngOnInit() {
  }
  register() {
    console.log(this.model);
    this.authService.register(this.model).subscribe(next => {
      console.log("register in successfully")
    }, error => {
      console.log('Failed to register');
    });
  }
  cancel() {
    this.cancelRegister.emit(false);
    console.log('canceled');
  }
  
}
