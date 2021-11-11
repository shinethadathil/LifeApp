import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-weatherforcast',
  templateUrl: './weatherforcast.component.html',
  styleUrls: ['./weatherforcast.component.css']
})
export class WeatherforcastComponent implements OnInit {
  weatherforcasts : any;
  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.getWeatherForcasts();
  }

  getWeatherForcasts(){
    this.http.get('http://localhost:5000/weatherforecast').subscribe(response => {
      this.weatherforcasts = response;
      console.log(this.weatherforcasts);
    }, error => {
      console.log(error);
    });
  }

}
