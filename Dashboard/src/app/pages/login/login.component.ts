import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { Subscription } from 'rxjs';
import { AuthSucceededResponse } from 'src/app/models/responses/auth-response-models';
import { AuthService } from 'src/app/services/auth.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { JwtHelperService } from "@auth0/angular-jwt";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  loginForm!: FormGroup;
  returnUrl: string = '';
  loginInvalid: boolean = false;
  errorMessage: string = '';
  loginSubscription!: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private socialAuthService: SocialAuthService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private tokenService: TokenStorageService
  ) {
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/home';
  }

  ngOnInit() {
    if (this.tokenService.getToken != null) {
      this.router.navigate([this.returnUrl]);
    }

    this.loginForm = this.formBuilder.group({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });

    this.loginSubscription = this.socialAuthService.authState.subscribe((user) => {
      this.loginInvalid = user == null;
      this.authService.loginWithGoogle(user.idToken).subscribe(l => {
        console.log(l);

        this.afterLogin(l);
      }, ex => {
        this.loginInvalid = true;
        console.log(ex);

      })
    });
  }

  loginWithGoogle(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  onSubmit(): void {
    this.loginInvalid = false;
    if (this.loginForm.valid) {
      const username = this.loginForm.get('username')?.value;
      const password = this.loginForm.get('password')?.value;
      this.authService.login(username, password).subscribe(l => {
        this.afterLogin(l);
      }, ex => {
        this.loginInvalid = true;
      })
    }
  }

  afterLogin(result: AuthSucceededResponse): void {
    const jwtHelper = new JwtHelperService();
    const decodedToken = jwtHelper.decodeToken(result.tokenInfo.accessToken);

    var role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    var roles = <[]>role;
    if ((Array.isArray(roles) && roles.some(e => e === 'Admin')) || role === 'Admin') {
      this.tokenService.saveToken(result.tokenInfo);
      this.router.navigate([this.returnUrl]);
    }
    else {
      this.loginInvalid = true;
      this.errorMessage = "User is not admin."
    }
  }

  ngOnDestroy(): void {
    this.loginSubscription.unsubscribe();
  }
}