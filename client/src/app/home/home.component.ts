import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Chart, ChartConfiguration, ChartOptions } from 'chart.js';
import { VirtualBatterySystem } from '../_models/VBS';
import { VBSGraphDataset } from '../_models/VBSGraphData';
import { AccountService } from '../_services/account.service';
import { VbsService } from '../_services/vbs.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  defaultSystem: VirtualBatterySystem;
  systemChargeLevel: number;

  constructor(private vbs: VbsService, private route: ActivatedRoute, private accountService: AccountService) { }

  ngOnInit(): void {
    this.vbs.getVBS(this.accountService.getUserDefaultSystemId()).subscribe({
      next: (response: VirtualBatterySystem) => {
        this.defaultSystem = response;
        this.systemChargeLevel = Number.parseFloat(response.chargePercentage);
      },
      error: e => {
        console.log(e);
      }
    })
  }
}
