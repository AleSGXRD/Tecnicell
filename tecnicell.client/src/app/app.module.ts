import { HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { NgModule  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {provideNativeDateAdapter} from '@angular/material/core';

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
import { AccessoriesTableComponent } from './Layouts/Accessory/accessories-table/accessories-table.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import { FormFieldComponent } from './Components/form-field/form-field.component';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatAccordion, MatExpansionModule} from '@angular/material/expansion';
import {MatIconModule} from '@angular/material/icon';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { FormFieldConditionerComponent } from './Components/form-field-conditioner/form-field-conditioner.component';
import { AccessoryElementComponent } from './Layouts/Accessory/accessory-element/accessory-element.component';
import { BatteriesTableComponent } from './Layouts/Battery/batteries-table/batteries-table.component';
import { BatteryElementComponent } from './Layouts/Battery/battery-element/battery-element.component';
import { BatteryBrandsTableComponent } from './Layouts/Battery/battery-brands-table/battery-brands-table.component';
import { ScreensTableComponent } from './Layouts/Screen/screens-table/screens-table.component';
import { ScreenElementComponent } from './Layouts/Screen/screen-element/screen-element.component';
import { PhonesTableComponent } from './Layouts/Phone/phones-table/phones-table.component';
import { PhoneElementComponent } from './Layouts/Phone/phone-element/phone-element.component';
import { PhoneRepairsTableComponent } from './Layouts/PhoneRepair/phone-repairs-table/phone-repairs-table.component';
import { PhoneRepairElementComponent } from './Layouts/PhoneRepair/phone-repair-element/phone-repair-element.component';

import {MatToolbarModule} from '@angular/material/toolbar';
import { ToolbarComponent } from './Components/toolbar/toolbar.component';
import { BackgroundComponent } from './Components/background/background.component';
import { NavigationMobileComponent } from './Components/navigation-mobile/navigation-mobile.component';
import { AccessoryHistoriesComponent } from './Layouts/Accessory/accessory-histories/accessory-histories.component';
import { LoginComponent } from './Layouts/login/login.component';
import { apiRequestInterceptor } from './Interceptor/api-request.interceptor';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { UserAccountTableComponent } from './Layouts/UserAccount/user-account-table/user-account-table.component';
import { NotificationBubbleComponent } from './Components/notification-bubble/notification-bubble.component';
import { ReloadComponentDirective } from './Directives/reload-component.directive';
import { AccessoryTypesTableComponent } from './Layouts/Accessory/accessory-types-table/accessory-types-table.component';
import { CurrenciesTableComponent } from './Layouts/Extras/currencies-table/currencies-table.component';
import { BranchesTableComponent } from './Layouts/Extras/branches-table/branches-table.component';
import { DashboardComponent } from './Layouts/Dashboard/dashboard/dashboard.component';
import { BatteryHistoriesComponent } from './Layouts/Battery/battery-histories/battery-histories.component';
import { ScreenHistoriesComponent } from './Layouts/Screen/screen-histories/screen-histories.component';
import { PhoneHistoriesComponent } from './Layouts/Phone/phone-histories/phone-histories.component';
import { PhoneRepairHistoriesComponent } from './Layouts/PhoneRepair/phone-repair-histories/phone-repair-histories.component';
import { SearchsTableComponent } from './Layouts/Extras/searchs-table/searchs-table.component';
import { PreformanceComponent } from './Layouts/Preformance/preformance/preformance.component';
import { TableFieldComponent } from './Components/table-field/table-field.component';
import { SuppliersTableComponent } from './Layouts/Extras/suppliers-table/suppliers-table.component';
import { WorkTypesTableComponent } from './Layouts/Extras/work-types-table/work-types-table.component';
import { DiaryWorksTableComponent } from './Layouts/DiaryWork/diary-works-table/diary-works-table.component';
import { DbManagerComponent } from './Layouts/Db/db-manager/db-manager.component';

@NgModule({
  declarations: [
    AppComponent, SidebarComponent,
    ButtonComponent, DirectionComponent, UserInfoComponent, OptionsComponent,
    TableComponent, DialogComponent, DialogDeleteComponent, FormComponent,
    AccessoriesTableComponent,
    FormFieldComponent,
    FormFieldConditionerComponent,
    AccessoryElementComponent,
    BatteriesTableComponent,
    BatteryElementComponent,
    BatteryBrandsTableComponent,
    ScreensTableComponent,
    ScreenElementComponent,
    PhonesTableComponent,
    PhoneElementComponent,
    PhoneRepairsTableComponent,
    PhoneRepairElementComponent,
    ToolbarComponent,
    BackgroundComponent,
    NavigationMobileComponent,
    AccessoryHistoriesComponent,
    LoginComponent,
    UserAccountTableComponent,
    NotificationBubbleComponent,
    ReloadComponentDirective,
    AccessoryTypesTableComponent,
    CurrenciesTableComponent,
    BranchesTableComponent,
    DashboardComponent,
    BatteryHistoriesComponent,
    ScreenHistoriesComponent,
    PhoneHistoriesComponent,
    PhoneRepairHistoriesComponent,
    SearchsTableComponent,
    PreformanceComponent,
    TableFieldComponent,
    SuppliersTableComponent,
    WorkTypesTableComponent,
    DiaryWorksTableComponent,
    DbManagerComponent,
     
  ],
  imports: [
    CommonModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule, CommonModule, RouterOutlet,
    ReactiveFormsModule,
    FormsModule, MatInputModule, MatFormFieldModule, MatSelectModule,MatSlideToggleModule,
    MatExpansionModule, MatAccordion, MatIconModule,MatDatepickerModule, MatToolbarModule, MatMenuModule, MatButtonModule
  ],
  providers: [
    provideAnimationsAsync(),
    provideNativeDateAdapter(),
    provideHttpClient(withInterceptors([apiRequestInterceptor])),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
