import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class UserDetailResolver implements Resolve<User> {

    constructor(private userService: UserService,
                private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.GetUser(route.params.id).pipe(
            catchError(error => {
                this.alertify.error('Getting data issue');
                this.router.navigate(['/users']);
                return of(null);
            })
        );
    }
// tslint:disable-next-line:eofline
}