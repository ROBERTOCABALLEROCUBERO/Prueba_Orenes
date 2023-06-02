/* tslint:disable */
/* eslint-disable */
import { Pedido } from './pedido';
import { Vehiculo } from './vehiculo';
export interface Ubicacion {
  fechaHora?: Date;
  latitud?: number;
  longitud?: number;
  pedido?: Pedido;
  pedidoId?: number;
  ubicacionId?: number;
  vehiculo?: Vehiculo;
  vehiculoId?: number;
}
