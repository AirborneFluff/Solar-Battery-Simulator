import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ChartDataset } from 'chart.js';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VbsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) { }

  getTodaysHistory() {
    return this.http.get(this.baseUrl + "vbs/1/history/today")
  }
}
