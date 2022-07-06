import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChartConfiguration, ChartOptions } from 'chart.js';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  title = 'client';
  currentUser: User;

  constructor(private accountService: AccountService, private router: Router) {

  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));

    if (user) {
      this.accountService.setCurrentUser(user);
      this.currentUser = user;
    } else {
      this.router.navigate(["/", "login"]);
    }
  }

  public lineChartData: ChartConfiguration<'line'>['data'] = {
    labels: [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July'
    ],
    datasets: [
      {
        data: [ 65, 59, 80, 81, 56, 55, 40 ],
        label: 'Series A',
        fill: true,
        tension: 0.5,
        borderColor: 'black',
        backgroundColor: 'rgba(255,0,0,0.3)'
      }
    ]
  };
  public lineChartOptions: ChartOptions<'line'> = {
    responsive: false
  };
  public lineChartLegend = true;
  public lineChartPlugins = [];
}
