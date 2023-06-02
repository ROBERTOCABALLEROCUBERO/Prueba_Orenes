import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { LoginComponent } from './login/login.component';
import { ClienteComponent } from './cliente/cliente.component';
import { AccesoVehiculoComponent } from './acceso-vehiculo/acceso-vehiculo.component';
import { VehiculoComponent } from './vehiculo/vehiculo.component';

const routes: Routes = [ {path: '', component: IndexComponent }, {path: 'login', component: LoginComponent }, {path: 'cliente', component: ClienteComponent }
,{path: 'Accesovehiculo', component: AccesoVehiculoComponent }, {path: 'vehiculo', component: VehiculoComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
