import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  rememberEmail: boolean = false;
  redirectParams: Params;

  constructor(public accountService: AccountService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.initializeForm();
    this.redirectParams = this.route.snapshot.queryParams['redirectURL'];
  }

  login() {
    if (this.rememberEmail)
      localStorage.setItem('loginEmail', JSON.stringify(this.loginForm.value.email));

    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        if (this.redirectParams) {
          this.router.navigate([this.redirectParams])
        } else {
          this.router.navigate(["home"]);
        }
      },
      error: error => {
        if (error.error === "No account found with this email/password combination") this.loginForm.setErrors({ invalidCombination: true })
        console.log(this.loginForm)
      }
    });
  }

  logout() {
    this.accountService.logout();
  }

  initializeForm() {
    this.loginForm = this.fb.group({
      email: ["", [Validators.email, Validators.required]],
      password: ["", Validators.required]
    })

    this.getStoredEmail();
  }

  getStoredEmail() {
    const email: string = JSON.parse(localStorage.getItem('loginEmail'));

    if (email) {
      this.rememberEmail = true;
      this.loginForm.setValue({ email: email, password: "" });
    }
  }

  setRememberMe() {
    this.rememberEmail = !this.rememberEmail
    if (!this.rememberEmail) {
      localStorage.removeItem('loginEmail');
      return;
    }
  }

}
