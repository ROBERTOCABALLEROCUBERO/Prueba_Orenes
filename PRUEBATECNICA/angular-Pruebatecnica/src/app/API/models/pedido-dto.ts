/* tslint:disable */
/* eslint-disable */
import { EstadoPedido } from './estado-pedido';

export interface PedidoDto {
  direccionEntrega?: null | string;
  status?: EstadoPedido;
}
