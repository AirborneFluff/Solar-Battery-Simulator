import { Component, OnInit } from '@angular/core';
import { Forecast } from '../_models/forecast';
import { DevService } from '../_services/dev.service';

@Component({
  selector: 'app-dev',
  templateUrl: './dev.component.html',
  styleUrls: ['./dev.component.css']
})
export class DevComponent implements OnInit {
  forecasts: Forecast[] = [];

  constructor(private devSerice: DevService) { }

  ngOnInit(): void {
  }

  getWeather() {
    this.devSerice.getWeather().subscribe(response => {
      this.forecasts = response;
    })
  }

}
