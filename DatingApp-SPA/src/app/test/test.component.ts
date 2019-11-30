import { Component, OnInit } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { from } from 'rxjs';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
Weathers: any ; 
  constructor(private http : HttpClient) { 
  }
  ngOnInit() {
    this.getWeather();
  }
  getWeather(){
    this.http.get("http://localhost:5000/WeatherForecast").subscribe(response=>{
      this.Weathers=response;
      console.table(response);
    }, error=>{
      console.log(error );
    }); 
  }
}
