import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Forecast } from '../_models/forecast';

@Injectable({
  providedIn: 'root'
})
export class DevService {
  baseUrl = "https://localhost:5001/api/"

  constructor(private http: HttpClient) { }

  getWeather(): Observable<Forecast[]> {
    return this.http.get<Forecast[]>(this.baseUrl + "WeatherForecast");
  }
}
