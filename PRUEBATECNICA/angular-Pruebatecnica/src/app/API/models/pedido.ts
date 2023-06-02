/* tslint:disable */
/* eslint-disable */
import { Cliente } from './cliente';
import { EstadoPedido } from './estado-pedido';
import { Ubicacion } from './ubicacion';
export interface Pedido {
  cliente?: Cliente;
  clienteId?: number;
  direccionEntrega?: null | string;
  pedidoId?: number;
  vehiculoId?: number;
  status?: EstadoPedido;
  ubicaciones?: null | Array<Ubicacion>;
}


