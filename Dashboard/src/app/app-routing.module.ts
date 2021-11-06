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
      { path: 'home', component: HomeComponent },
      {
        path: 'ads',
        children: [
          { path: '', component: AdComponent, pathMatch: 'full' },
          { path: ':id', component: AdViewComponent },
        ]
      },
      { path: 'ads', component: AdViewComponent },
      { path: 'books', component: BooksComponent },
      { path: 'dream-categories', component: DreamCategoriesComponent },
      { path: 'pots', component: PostComponent },
      { path: 'post-categories', component: PostCategoriesComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'statistics', component: StatisticsComponent },
      { path: 'users', component: UserComponent },
      { path: 'words', component: WordsComponent },
      { path: '**', component: NotFoundComponent },
    ]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
