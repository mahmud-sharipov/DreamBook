import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppComponent } from './app.component';
import { SettingsComponent } from './components/settings/settings.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { MenuComponent } from './components/menu/menu.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { DreamCategoriesComponent } from './pages/dream-categories/dream-categories.component';
import { BooksComponent } from './pages/books/books.component';
import { WordsComponent } from './pages/words/words.component';
import { PostComponent } from './pages/post/post.component';
import { PostCategoriesComponent } from './pages/post-categories/post-categories.component';
import { AdComponent } from './pages/ad/ad.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { StatisticsComponent } from './pages/statistics/statistics.component';
import { UserComponent } from './pages/user/user.component';
import { NotFoundComponent } from './pages/not-found/not-found.component'
import { RegisterComponent } from './pages/register/register.component';
import { AuthGuard } from './guards/auth.guard';
import { DashboardLayoutComponent } from './_layouts/dashboard-layout/dashboard-layout.component';
import { API_BASE_URL, getBaseApiUrl } from './services/end-point';
import { headerInterceptorProviders } from './interceptors/header.interceptor';
import { WordComponent } from './pages/words/word/word.component';
import { WordFormComponent } from './pages/words/word/word-form/word-form.component';
import { PagedListComponent } from './components/paged-list/paged-list.component';
import { BookComponent } from './pages/books/book/book.component';
import { BookFormComponent } from './pages/books/book/book-form/book-form.component';
import { DreamCategoryComponent } from './pages/dream-categories/dream-category/dream-category.component';
import { DreamCategoryFormComponent } from './pages/dream-categories/dream-category/dream-category-form/dream-category-form.component';
import { ColorPickerModule } from 'ngx-color-picker';
import { PostCategoryFormComponent } from './pages/post-categories/post-category/post-category-form/post-category-form.component';
import { PostCategoryComponent } from './pages/post-categories/post-category/post-category.component';
import { AdViewComponent } from './pages/ad/ad-view/ad-view.component';
import { AdCardComponent } from './pages/ad/ad-card/ad-card.component';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { authInterceptorProviders } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SettingsComponent,
    MenuComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    DreamCategoriesComponent,
    BooksComponent,
    WordsComponent,
    PostComponent,
    PostCategoriesComponent,
    AdComponent,
    ProfileComponent,
    StatisticsComponent,
    UserComponent,
    NotFoundComponent,
    RegisterComponent,
    DashboardLayoutComponent,
    WordComponent,
    WordFormComponent,
    PagedListComponent,
    BookComponent,
    BookFormComponent,
    DreamCategoryComponent,
    DreamCategoryFormComponent,
    PostCategoryFormComponent,
    PostCategoryComponent,
    AdViewComponent,
    AdCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    ColorPickerModule,
    SocialLoginModule
  ],
  providers: [
    { provide: API_BASE_URL, useFactory: getBaseApiUrl },
    headerInterceptorProviders,
    authInterceptorProviders,
    AuthGuard,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '212273882569-8m7fj3jjf4mka7cjjo4h6b4ivi7c0sv7.apps.googleusercontent.com'
            )
          }
        ]
      } as SocialAuthServiceConfig,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
