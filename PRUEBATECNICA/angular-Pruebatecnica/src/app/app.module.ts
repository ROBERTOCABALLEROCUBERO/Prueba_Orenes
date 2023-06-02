import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexComponent } from './index/index.component';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ClienteComponent } from './cliente/cliente.component';
import { AccesoVehiculoComponent } from './acceso-vehiculo/acceso-vehiculo.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { VehiculoComponent } from './vehiculo/vehiculo.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    LoginComponent,
    NavbarComponent,
    ClienteComponent,
    AccesoVehiculoComponent,
    VehiculoComponent,
 
    
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
