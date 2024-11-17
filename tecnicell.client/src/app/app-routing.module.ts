import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccessoriesTableComponent } from './Layouts/Accessory/accessories-table/accessories-table.component';
import { AccessoryElementComponent } from './Layouts/Accessory/accessory-element/accessory-element.component';
import { BatteryBrandsTableComponent } from './Layouts/Battery/battery-brands-table/battery-brands-table.component';
import { BatteriesTableComponent } from './Layouts/Battery/batteries-table/batteries-table.component';
import { BatteryElementComponent } from './Layouts/Battery/battery-element/battery-element.component';
import { ScreensTableComponent } from './Layouts/Screen/screens-table/screens-table.component';
import { ScreenElementComponent } from './Layouts/Screen/screen-element/screen-element.component';
import { PhonesTableComponent } from './Layouts/Phone/phones-table/phones-table.component';
import { PhoneElementComponent } from './Layouts/Phone/phone-element/phone-element.component';
import { PhoneRepairsTableComponent } from './Layouts/PhoneRepair/phone-repairs-table/phone-repairs-table.component';
import { PhoneRepairElementComponent } from './Layouts/PhoneRepair/phone-repair-element/phone-repair-element.component';
import { AccessoryHistoriesComponent } from './Layouts/Accessory/accessory-histories/accessory-histories.component';
import { LoginComponent } from './Layouts/login/login.component';
import { AuthGuard } from './Auth/auth.guard';
import { UserAccountTableComponent } from './Layouts/UserAccount/user-account-table/user-account-table.component';
import { AccessoryTypesTableComponent } from './Layouts/Accessory/accessory-types-table/accessory-types-table.component';
import { CurrenciesTableComponent } from './Layouts/Extras/currencies-table/currencies-table.component';
import { BranchesTableComponent } from './Layouts/Extras/branches-table/branches-table.component';
import { DashboardComponent } from './Layouts/Dashboard/dashboard/dashboard.component';
import { BatteryHistoriesComponent } from './Layouts/Battery/battery-histories/battery-histories.component';
import { ScreenHistoriesComponent } from './Layouts/Screen/screen-histories/screen-histories.component';
import { PhoneHistoryApiService } from './Services/api/Phone/phone-history-api.service';
import { PhoneHistoriesComponent } from './Layouts/Phone/phone-histories/phone-histories.component';
import { PhoneRepairHistoriesComponent } from './Layouts/PhoneRepair/phone-repair-histories/phone-repair-histories.component';
import { SearchsTableComponent } from './Layouts/Extras/searchs-table/searchs-table.component';
import { PreformanceComponent } from './Layouts/Preformance/preformance/preformance.component';
import { SuppliersTableComponent } from './Layouts/Extras/suppliers-table/suppliers-table.component';
import { WorkTypesTableComponent } from './Layouts/Extras/work-types-table/work-types-table.component';
import { DiaryWorksTableComponent } from './Layouts/DiaryWork/diary-works-table/diary-works-table.component';
import { DbManagerComponent } from './Layouts/Db/db-manager/db-manager.component';

const routes: Routes = [
  {
    path:'',
    pathMatch:'prefix',
    redirectTo: 'dashboard'
  },

  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: 'useraccounts',
    component : UserAccountTableComponent,
    canActivate: [AuthGuard]
  },
  
  {
    path:'accessories',
    component: AccessoriesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'accessory/:id',
    component: AccessoryElementComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'accessory-histories',
    component: AccessoryHistoriesComponent,
    canActivate: [AuthGuard]
  },

  {
    path:'batteries',
    component: BatteriesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'battery/:id',
    component: BatteryElementComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'battery-histories',
    component: BatteryHistoriesComponent,
    canActivate: [AuthGuard]
  },

  {
    path:'phones',
    component: PhonesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'phone/:id',
    component: PhoneElementComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'phone-histories',
    component: PhoneHistoriesComponent,
    canActivate: [AuthGuard]
  },
  
  {
    path:'phonesrepair',
    component: PhoneRepairsTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'phonerepair/:id',
    component: PhoneRepairElementComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'phonerepair-histories',
    component: PhoneRepairHistoriesComponent,
    canActivate: [AuthGuard]
  },

  {
    path:'screens',
    component: ScreensTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'screen/:id',
    component: ScreenElementComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'screen-histories',
    component: ScreenHistoriesComponent,
    canActivate: [AuthGuard]
  },

  {
    path:'batterybrands',
    component: BatteryBrandsTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'accessorytypes',
    component: AccessoryTypesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'currencies',
    component: CurrenciesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'branches',
    component: BranchesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'preformance',
    component: PreformanceComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'searchs',
    component: SearchsTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'suppliers',
    component: SuppliersTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'worktypes',
    component: WorkTypesTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'diaryworks',
    component: DiaryWorksTableComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'db',
    component: DbManagerComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
