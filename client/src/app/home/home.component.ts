import { Component, OnInit } from '@angular/core';
import { Chart, ChartConfiguration, ChartOptions } from 'chart.js';
import { VBSGraphDataset } from '../_models/VBSGraphData';
import { VbsService } from '../_services/vbs.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  values = [
    "3:00 am",
    "7:00 am",
    "11:00 am",
    "3:00 pm",
    "7:00 pm",
    "11:00 pm",
  ]
  public lineChartOptions: ChartOptions<'line'> = {
    responsive: true,
  };
  public lineChartLegend = true;
  public lineChartPlugins = [];

  lineChartData: ChartConfiguration<'line'>['data'];

  constructor(private vbs: VbsService) { }

  ngOnInit(): void {
    this.vbs.getTodaysHistory().subscribe({
      next: (response: VBSGraphDataset[]) => {
        this.lineChartData = {
          labels: response.map(e => this.convertSecondsToHours(e.x)),
          datasets: [
            {
              data: response.map(e => e.cl),
              label: 'Charge Level',
              fill: false,
              tension: 0.5,
              borderColor: 'black',
              backgroundColor: 'rgba(255,0,0,0.3)',
              type: "line",
            },
            {
              data: response.map(e => e.ri),
              label: 'Real Import',
              fill: false,
              tension: 0.5,
              borderColor: 'black',
              backgroundColor: 'rgba(255,0,0,0.3)',
            }
          ]
        };
      }
    })
  }

  convertSecondsToHours(seconds: number): string {
    const date = new Date();
    date.setSeconds(seconds);
    return `${date.getHours().toString()}:${date.getMinutes().toString()}:00`;
  }

}
