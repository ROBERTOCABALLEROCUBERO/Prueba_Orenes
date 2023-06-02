/* tslint:disable */
/* eslint-disable */
import { Pedido } from './pedido';
export interface Cliente {
  clienteId?: number;
  nombre?: null | string;
  password?: null | string;
  pedidos?: null | Array<Pedido>;
}
