import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AdViewComponent } from './pages/ad/ad-view/ad-view.component';
import { AdComponent } from './pages/ad/ad.component';
import { BooksComponent } from './pages/books/books.component';
import { DreamCategoriesComponent } from './pages/dream-categories/dream-categories.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { PostCategoriesComponent } from './pages/post-categories/post-categories.component';
import { PostComponent } from './pages/post/post.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { StatisticsComponent } from './pages/statistics/statistics.component';
import { UserComponent } from './pages/user/user.component';
import { WordsComponent } from './pages/words/words.component';
import { DashboardLayoutComponent } from './_layouts/dashboard-layout/dashboard-layout.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },

  //Dashboard routs
  {
    path: '',
    component: DashboardLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
      {
        path: 'ads',
        children: [
          { path: '', component: AdComponent, pathMatch: 'full', canActivate: [AuthGuard] },
          { path: ':id', component: AdViewComponent, canActivate: [AuthGuard] },
        ]
      },
      { path: 'ads', component: AdViewComponent, canActivate: [AuthGuard] },
      { path: 'books', component: BooksComponent, canActivate: [AuthGuard] },
      { path: 'dream-categories', component: DreamCategoriesComponent, canActivate: [AuthGuard] },
      { path: 'pots', component: PostComponent, canActivate: [AuthGuard] },
      { path: 'post-categories', component: PostCategoriesComponent, canActivate: [AuthGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'statistics', component: StatisticsComponent, canActivate: [AuthGuard] },
      { path: 'users', component: UserComponent, canActivate: [AuthGuard] },
      { path: 'words', component: WordsComponent, canActivate: [AuthGuard] },
      { path: '**', component: NotFoundComponent, canActivate: [AuthGuard] },
    ]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
