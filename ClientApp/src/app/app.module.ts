import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { MainComponent } from './components/main/main.component';
import { HeaderComponent } from './components/header/header.component';
import { BodyComponent } from './components/body/body.component';
import { LoginComponent } from './components/login/login.component';
import { ShortenerComponent } from './components/shortener/shortener.component';
import { ListComponent } from './components/list/list.component';
import { SettingsComponent } from './components/settings/settings.component';
import { AboutComponent } from './components/about/about.component';
import { MessageComponent } from './components/message/message.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    HeaderComponent,
    BodyComponent,
    LoginComponent,
    ShortenerComponent,
    ListComponent,
    SettingsComponent,
    AboutComponent,
    MessageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { component: LoginComponent, path: "login" },
      { component: ShortenerComponent, path: "shortener"},
      { component: ListComponent, path: "list" },
      { component: SettingsComponent, path: "settings" },
      { component: AboutComponent, path: "about" }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
