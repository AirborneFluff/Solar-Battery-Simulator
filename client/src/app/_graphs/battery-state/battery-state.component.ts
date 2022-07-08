import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ChartConfiguration, Color } from 'chart.js';

@Component({
  selector: 'app-battery-state',
  templateUrl: './battery-state.component.html',
  styleUrls: ['./battery-state.component.css']
})
export class BatteryStateComponent implements OnChanges {
  @Input() chargeLevel: number = 0;
  doughnutData: any;

  constructor() {

  }
  ngOnChanges(changes: SimpleChanges): void {
    this.initGraph()
  }

  initGraph() {
    this.doughnutData = {
      datasets: [{
        data: [this.chargeLevel, 100 - this.chargeLevel],
        backgroundColor: [
          'rgb(0, 255, 0)',
          'rgb(233, 233, 233)',
        ]
      }]
    };
  }
}
