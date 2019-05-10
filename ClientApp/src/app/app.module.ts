import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';

import { AboutComponent } from './about/about.component';
import { AppComponent } from './app.component';
import { ConfirmComponent } from './common/confirm.component';
import { DetailComponent } from './pokemon/detail.component';
import { HomeComponent } from './home/home.component';
import { ListComponent }     from './pokemon/list.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { PokemonService } from './pokemon/pokemon.service';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    ConfirmComponent,
    HomeComponent,
    DetailComponent,
    ListComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    HttpClientModule,
    MatDialogModule,
    MatTooltipModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'about', component: AboutComponent },
      { path: 'list', component: ListComponent },
      { path: 'detail/:id', component: DetailComponent },
      { path: 'detail/add', component: DetailComponent }
    ])
  ],
  entryComponents: [
    ConfirmComponent
  ],
  providers: [
    PokemonService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
