import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(["/login"]);
  }

}
