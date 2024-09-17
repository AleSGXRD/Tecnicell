import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AppComponent } from './app.component';
import { SidebarComponent } from './Components/sidebar/sidebar.component';
import { ButtonComponent } from './Components/buttons/button/button.component';
import { DirectionComponent } from './Components/direction/direction.component';
import { UserInfoComponent } from './Components/user-info/user-info.component';
import { OptionsComponent } from './Components/options/options.component';
import { TableComponent } from './Components/table/table.component';
import { DialogComponent } from './Components/dialogs/dialog/dialog.component';
import { DialogDeleteComponent } from './Components/dialogs/dialog-delete/dialog-delete.component';
import { FormComponent } from './Components/form/form.component';
@NgModule({
  declarations: [
    AppComponent, SidebarComponent,
    ButtonComponent, DirectionComponent, UserInfoComponent, OptionsComponent,
    TableComponent, DialogComponent, DialogDeleteComponent, FormComponent
  ],
  imports: [
    CommonModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule, CommonModule, RouterOutlet,
    ReactiveFormsModule,
    FormsModule, 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
