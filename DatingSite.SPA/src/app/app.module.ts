import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';

import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { UserService } from './_services/user.service';
import { UserListComponent } from './users/user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UserCardComponent } from './users/user-card/user-card.component';
import { UserDatailComponent } from './users/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { PreventUnsavesChanges } from './_guards/prevent-unsaves-changes.guard';
import { PhotosComponent } from './users/photos/photos.component';
import { TimeAgoPipe } from './_pipes/time-ago-pipe';
import { LikesResolver } from './_resolvers/likes.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { UserMessagesComponent } from './users/user-messages/user-messages.component';



export function getToken() {
   return localStorage.getItem('token');
}


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      UserListComponent,
      LikesComponent,
      MessagesComponent,
      UserCardComponent,
      UserDatailComponent,
      UserEditComponent,
      PhotosComponent,
      TimeAgoPipe,
      UserMessagesComponent
   ],
   imports: [
      HttpClientModule,
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      NgxGalleryModule,
      FileUploadModule,
      JwtModule.forRoot({
         config: {
           tokenGetter: getToken,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
         }
       }),
       RouterModule.forRoot(appRoutes),
       BsDropdownModule.forRoot(),
       TabsModule.forRoot(),
       PaginationModule.forRoot(),
       BrowserAnimationsModule,
       BsDatepickerModule.forRoot(),
       ButtonsModule.forRoot()

   ],
   providers: [
      AuthService,
      AlertifyService,
      UserService,
      AuthGuard,
      ErrorInterceptorProvider,
      UserDetailResolver,
      UserListResolver,
      UserDetailResolver,
      UserEditResolver,
      PreventUnsavesChanges,
      LikesResolver,
      MessagesResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
