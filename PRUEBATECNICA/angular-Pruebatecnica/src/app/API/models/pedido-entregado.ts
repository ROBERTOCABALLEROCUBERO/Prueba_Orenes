/* tslint:disable */
/* eslint-disable */
import { Cliente } from './cliente';
import { EstadoPedido } from './estado-pedido';
export interface PedidoEntregado {
  cliente?: Cliente;
  clienteId?: number;
  direccionEntrega?: null | string;
  pedidoEntregadoId?: number;
  status?: EstadoPedido;
}
