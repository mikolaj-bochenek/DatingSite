import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class UserListResolver implements Resolve<User[]> {

    pageNumber = 1;
    pageSize = 18;

    constructor(private userService: UserService,
                private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.GetUsers(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Getting data issue');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
