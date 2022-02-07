import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDatailComponent } from './users/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { PreventUnsavesChanges } from './_guards/prevent-unsaves-changes.guard';
import { LikesResolver } from './_resolvers/likes.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
        { path: 'users', component: UserListComponent,
                        resolve: {users: UserListResolver}},
        { path: 'users/:id', component: UserDatailComponent,
                            resolve: {user: UserDetailResolver}},
        { path: 'user/edit', component: UserEditComponent,
                            resolve: {user: UserEditResolver},
                            canDeactivate: [PreventUnsavesChanges]},
        { path: 'likes', component: LikesComponent,
                         resolve: {users: LikesResolver}},
        { path: 'messages', component: MessagesComponent,
                            resolve: {messages: MessagesResolver} },
    ]},
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
