import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import axios from 'axios';
import { Pedido, PedidoDto, PedidoEntregado } from '../API/models';
import { EstadoPedido } from '../API/models';
import { from } from 'rxjs/internal/observable/from';
import { PedidoService } from '../API/services';
import { PedidosEntregadosService } from '../API/services';
import { UbicacionService } from '../API/services';
import { PedidoWithInfo } from '../API/models/pedidoinfo';
import { Ubicacion } from '../API/models';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  pedidosEntregados: PedidoEntregado[] = [];
  clienteForm: FormGroup;
  localidades: string[] = [];
  calles: string[] = [];
  pedidos: PedidoWithInfo[] = [];
  pedidosaImprimir: PedidoWithInfo[] = []
  constructor(private formBuilder: FormBuilder, private pedidoService: PedidoService, private pedidosEntregadoService: PedidosEntregadosService, private ubicacionService: UbicacionService, private changeDetectorRef: ChangeDetectorRef) {
    this.clienteForm = this.formBuilder.group({
      localidadInput: ['', Validators.required],
      calleInput: ['', Validators.required],
     
    });
  }


  async obtenerTiempo(pedido: PedidoWithInfo, latitud: number, longitud: number) {
    try {
      const ubicacion: Ubicacion | undefined = await this.ubicacionService.obtenerUbicacionMasReciente(pedido.pedidoId!).toPromise();
      if (ubicacion) {
        const latitud2 = ubicacion.latitud;
        const longitud2 = ubicacion.longitud;
  
        // Utiliza latitud y longitud para calcular el tiempo restante
        const url = `https://router.project-osrm.org/route/v1/driving/${longitud},${latitud};${longitud2},${latitud2}`;
        const response = await axios.get(url);
        const tiempo = response.data.routes[0].duration;
  
        // Procesa el tiempo y realiza cualquier acción que desees
        const horas = Math.floor(tiempo / 3600);
        const minutos = Math.floor((tiempo % 3600) / 60);
        const segundos = Math.floor((tiempo % 3600) % 60);
  
        pedido.tiempo = { horas, minutos, segundos };
        console.log(pedido.tiempo);
  
      this.pedidosaImprimir.push(pedido);
    
      } else {
        // No se encontró una ubicación para el pedido
        console.error('No se encontró ubicación para el pedido:', pedido.pedidoId);
        
      }
    
    } catch (error) {
      this.pedidosaImprimir.push(pedido);
      // Maneja el error como desees
    }
  }




  buscarLocalidad(): void {
    const localidadInput = this.clienteForm.get('localidadInput')?.value;

    if (localidadInput.trim() !== '') {
      const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(localidadInput)}`;

      axios.get(url)
        .then((response: any) => {
          this.localidades = response.data.map((result: any) => result.display_name);
        })
        .catch((error: any) => {
          console.error('Error al obtener sugerencias de localidades:', error);
        });
    } else {
      this.localidades = [];
    }
  }

  ngOnInit() {
    this.obtenerPedidos();
    this.obtenerPedidosEntregados();
    setInterval(() => {
      this.obtenerPedidos(); 
      this.obtenerCoordenadas();// Ejecutar el método cada 20 segundos
    }, 20000);

  }
  
  buscarCalles(): void {
    const calleInput = this.clienteForm.get('calleInput')?.value;
    const localidadInput = this.clienteForm.get('localidadInput')?.value;
  
    if (calleInput.trim() !== '' && localidadInput.trim() !== '') {
      const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(calleInput)}, ${encodeURIComponent(localidadInput)}`;
  
      axios.get(url)
        .then((response: any) => {
          this.calles = response.data.map((result: any) => result.display_name);
        })
        .catch((error: any) => {
          console.error('Error al obtener sugerencias de calles:', error);
        });
    } else {
      this.calles = [];
    }
  }

  async obtenerCoordenadas(): Promise<{ latitud: number; longitud: number }[]> {
    try {
      this.pedidosaImprimir = [];
      const coordenadas: { latitud: number; longitud: number }[] = [];
  console.log("ESTOS SON MIS PEDIDOS A IMPRIMIR" + this.pedidos)
      for (const pedido of this.pedidos) {
        const direccionEntrega = pedido.direccionEntrega;
        const status = pedido.status;
  
        console.log("EL PEDIDO AL QUE LE BUSCO LAS COORDENADAS ES: " + pedido);
        console.log('Dirección de entrega:', direccionEntrega);
        console.log('Estado del pedido:', status);
  
        const direccion = `${direccionEntrega}`;
        const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(direccion)}`;
  
        const response = await axios.get(url);
        const resultado = response.data[0];
  
        if (resultado) {
          const latitud = parseFloat(resultado.lat);
          const longitud = parseFloat(resultado.lon);
  
          console.log('Coordenadas obtenidas:');
          console.log('Latitud:', latitud);
          console.log('Longitud:', longitud);
  
          coordenadas.push({ latitud, longitud });
  
          // Obtener tiempo para el pedido actual
          await this.obtenerTiempo(pedido, latitud, longitud);

        } else {
          throw new Error('No se encontraron coordenadas para la dirección proporcionada');
        }
      }
  
      return coordenadas;
    } catch (error) {
      throw new Error('Error al obtener las coordenadas');
    }
  }

 

  seleccionarLocalidad(localidad: string): void {
    this.clienteForm.get('localidadInput')?.setValue(localidad);
    this.localidades = [];
  }

  seleccionarCalle(calle: string): void {
    this.clienteForm.get('calleInput')?.setValue(calle);
    this.calles = [];
  }

  obtenerPedidosEntregados() {
    this.pedidosEntregadoService
      .obtenerPedidosPorCliente()
      .then((response) => {
        this.pedidosEntregados = response.data;
        console.log(this.pedidosEntregados);
      })
      .catch((error) => {
        console.error('Error al obtener los pedidos entregados:', error);
        // Manejar el error según tus necesidades
      });
  }

obtenerPedidos(): void {
  const token = localStorage.getItem('token'); // Obtener el token del localStorage
  if (token) {
    this.pedidoService.obtenerPedidosCliente(token).subscribe(
      (response: any) => {
        this.pedidos = response;
      },
      (error: any) => {
        console.error('Error al obtener los pedidos:', error);
      }
    );
  }}


  crearPedido(): void {
    if (this.clienteForm.valid) {

      const pedido: PedidoDto = {
        direccionEntrega: this.clienteForm.get('calleInput')?.value,
        status: 0,
      };
 
      from(this.pedidoService.crearPedido(pedido)).subscribe(
        (response: Pedido) => {
          console.log('Pedido creado:', response);
          // Realizar acciones adicionales después de crear el pedido, si es necesario
          this.clienteForm.reset(); // Reiniciar el formulario después de crear el pedido
        },
        (error: any) => {
          console.error('Error al crear el pedido:', error);
          // Mostrar mensaje de error al usuario si es necesario
        }
      );
    }
  
}}