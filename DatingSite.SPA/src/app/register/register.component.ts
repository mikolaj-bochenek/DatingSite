import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { Router } from '@angular/router';
declare let alertify: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  // tslint:disable-next-line:no-shadowed-variable
  constructor(private authService: AuthService, private alertify: AlertifyService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-orange'
    },
    // this.createRegisterForm();
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(10)]),
      confirmPassword: new FormControl('', Validators.required),
      gender: new FormControl('female'),
      dateOfBirth: new FormControl(null, Validators.required),
      city: new FormControl('', Validators.required),
      country: new FormControl('', Validators.required)
    }, this.passwordMatchValidator);
  }

  // createRegisterForm() {
  //   this.registerForm = this.fb.group({
  //     username: ['', Validators.required],
  //     password: ['', Validators.required, Validators.minLength(6), Validators.maxLength(10)],
  //     confirmPasswprd: ['', Validators.required]
  //   }, {validator: this.passwordMatchValidator});
  // }

  passwordMatchValidator(fg: FormControl) {
    return fg.get('password').value === fg.get('confirmPassword').value ? null : { mismatch: true };
  }

  register() {
    if (this.registerForm.valid) {

      this.user = Object.assign({}, this.registerForm.value);

      this.authService.register(this.user).subscribe(() => {
      this.alertify.success('Registered succesfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.authService.login(this.user).subscribe( () => {
        this.router.navigate(['/users']);
      });
    });
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alertify.warning('Registation cancelled');
  }
}
