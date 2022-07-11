import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ChartDataset } from 'chart.js';
import { environment } from 'src/environments/environment';
import { VirtualBatterySystem } from '../_models/VBS';
import { VBSGraphDataset } from '../_models/VBSGraphData';

@Injectable({
  providedIn: 'root'
})
export class VbsService {
  baseUrl = environment.apiUrl + "vbs/";

  constructor(private http: HttpClient, private router: Router) { }

  getTodaysHistory(id: number) {
    return this.http.get<VBSGraphDataset[]>(this.baseUrl + id + "/history/today")
  }

  getVBS(id: number) {
    return this.http.get<VirtualBatterySystem>(this.baseUrl + id)
  }
}
