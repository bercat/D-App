import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent}, /* path: blir localhost:4200 */
    {
        path: '', /*definerer tomt space før path-navnet*/
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard], /* lar alle være underlagt 1 guard*/
        children: [
            { path: 'members', component: MemberListComponent},
            { path: 'messages', component: MessagesComponent},
            { path: 'lists', component: ListsComponent},
        ]
    },   /* redirectTo: blir localhost:4200 */
    { path: '**', redirectTo: '', pathMatch: 'full'}, /*ønsker å matche full path av URL */
];
