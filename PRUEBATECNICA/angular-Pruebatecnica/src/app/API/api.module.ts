/* tslint:disable */
/* eslint-disable */
import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConfiguration, ApiConfigurationParams } from './api-configuration';

import { ClientesService } from './services/clientes.service';
import { PedidosService } from './services/pedidos.service';
import { PedidosEntregadosService } from './services/pedidos-entregados.service';
import { UbicacionesService } from './services/ubicaciones.service';
import { VehiculoService } from './services/vehiculos.service';

import { AccesoVehiculoComponent } from '../acceso-vehiculo/acceso-vehiculo.component';
/**
 * Module that provides all services and configuration.
 */
@NgModule({
  imports: [],
  exports: [],
  declarations: [],
  providers: [
    ClientesService,
    PedidosService,
    PedidosEntregadosService,
    UbicacionesService,
    VehiculoService,
    ApiConfiguration,

  ],
})
export class ApiModule {
  static forRoot(params: ApiConfigurationParams): ModuleWithProviders<ApiModule> {
    return {
      ngModule: ApiModule,
      providers: [
        {
          provide: ApiConfiguration,
          useValue: params
        }
      ]
    }
  }

  constructor( 
    @Optional() @SkipSelf() parentModule: ApiModule,
    @Optional() http: HttpClient
  ) {
    if (parentModule) {
      throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
    }
    if (!http) {
      throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
      'See also https://github.com/angular/angular/issues/20575');
    }
  }
}
