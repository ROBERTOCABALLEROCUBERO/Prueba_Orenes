import { Component, OnDestroy } from '@angular/core';
import { VehiculoService } from '../API/services/vehiculos.service';
import { PedidoService } from '../API/services/pedidos.service';
import { UbicacionService } from '../API/services/ubicaciones.service';
import { Router } from '@angular/router';
import { EstadoPedido, Pedido, Ubicacion } from '../API/models';
import { from } from 'rxjs';
import { AxiosResponse } from 'axios';
import axios from 'axios';
import { PedidoWithInfo } from '../API/models/pedidoinfo';

@Component({
  selector: 'app-vehiculo',
  templateUrl: './vehiculo.component.html',
  styleUrls: ['./vehiculo.component.css'],
})
export class VehiculoComponent implements OnDestroy {
  pedidosConInfo: PedidoWithInfo[] = [];
  pedidosEnProcesoConInfo: PedidoWithInfo[] = [];
  private geolocationWatch: any; // Variable para almacenar la suscripción a la geolocalización
  latitud: number = 0;
  longitud: number = 0;
  pedidos: Pedido[] = [];
  pedidosPendientes: Pedido[] = [];
  pedidosEnProceso: PedidoWithInfo[] = [];
  distancia: number = 0;
  tiempo: { horas: number; minutos: number; segundos: number } = {
    horas: 0,
    minutos: 0,
    segundos: 0,
  };
  ubicacion: Ubicacion = {};
  ubicaciones: any[] = [];
  ubicacionActualizada: any;

  constructor(
    private vehiculoService: VehiculoService,
    private pedidoService: PedidoService,
    private ubicacionService: UbicacionService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.iniciarObtencionUbicacion(); // Iniciar la obtención de ubicación al cargar el componente
    this.obtenerPedidosEstado0(); // Obtener los pedidos en estado 0
    this.actualizarUbicacionPeriodicamente(); // Actualizar la ubicación del pedido en proceso cada 5 minutos
    this.obtenerPedidosPorVehiculo();
    this.actualizarUbicacionesPedidos(); 
  }

  ngOnDestroy(): void {
    this.detenerObtencionUbicacion(); // Detener la obtención de ubicación al salir del componente
  }

  actualizarUbicacionesPedidos(){
    setInterval(() => {   
    this.pedidosEnProceso.forEach((pedido: any) => {
      this.ubicacionService
        .iniciarSeguimientoUbicacion(
          pedido.pedidoId,
          parseInt(localStorage.getItem('vehiculoId')!),
          this.longitud,
          this.latitud
        )
        .then(() => {
          console.log('Seguimiento de ubicación iniciado exitosamente para el pedido:', pedido.pedidoId);
          // Realiza cualquier otra acción que desees después de iniciar el seguimiento de ubicación para este pedido
        })
        .catch((error: any) => {
          console.error('Error al iniciar el seguimiento de ubicación para el pedido:', pedido.pedidoId);
          console.error('Error:', error);
          // Maneja el error como desees para este pedido
        });
    });
  }, 35000);




  }




  marcarPedidoEnProceso(pedido: PedidoWithInfo) {
    console.log('Marcar pedido como en proceso:', pedido.pedidoId);

    // Llamar al servicio para marcar el pedido como en proceso
    this.pedidoService
      .marcarPedidoEnProceso(pedido.pedidoId!, Number(localStorage.getItem('vehiculoId')))
      .then(() => {
        console.log('Pedido marcado como en proceso exitosamente');

        // Iniciar el seguimiento de ubicación para el pedido y vehículo
        this.ubicacionService
          .iniciarSeguimientoUbicacion(
            pedido.pedidoId!,
            parseInt(localStorage.getItem('vehiculoId')!),
            this.longitud,
            this.latitud
          )
          .then(() => {
            console.log('Seguimiento de ubicación iniciado exitosamente');
            // Realiza cualquier otra acción que desees después de iniciar el seguimiento de ubicación
          })
          .catch((error: any) => {
            console.error(
              'Error al iniciar el seguimiento de ubicación:',
              error
            );
            // Maneja el error como desees
          });
      })
      .catch((error: any) => {
        console.error('Error al marcar el pedido como en proceso:', error);
        // Maneja el error como desees
      });
  }

  private iniciarObtencionUbicacion(): void {
    if (navigator.geolocation) {
      this.geolocationWatch = navigator.geolocation.watchPosition(
        (position) => {
          console.log(position.coords.latitude, position.coords.longitude);
          this.actualizarUbicacion(
            position.coords.latitude,
            position.coords.longitude
          );
        },
        (error) => {
          console.error('Error al obtener la ubicación:', error);
        }
      );
    } else {
      console.error('La geolocalización no es compatible con este navegador.');
    }
  }

  private detenerObtencionUbicacion(): void {
    if (this.geolocationWatch) {
      navigator.geolocation.clearWatch(this.geolocationWatch);
    }
  }

  private actualizarUbicacion(latitud: number, longitud: number): void {
    const vehiculoId = Number(localStorage.getItem('vehiculoId'));

    if (!isNaN(vehiculoId)) {
      const vehiculo = {
        vehiculoId: vehiculoId,
        ubicacionLat: latitud,
        ubicacionLon: longitud,
        ubicaciones: [],
      };
      console.log(vehiculo);

      this.vehiculoService.actualizarVehiculo(vehiculoId, vehiculo).subscribe(
        () => {
          console.log('Ubicación actualizada');
          // Aquí puedes realizar acciones adicionales después de actualizar la ubicación, si lo deseas.
        },
        (error) => {
          console.error('Error al actualizar la ubicación:', error);
          // Aquí puedes mostrar un mensaje de error al usuario si lo deseas.
        }
      );
    }
  }

  private actualizarUbicacionPeriodicamente(): void {
    setInterval(
      () => {
        const vehiculoId = Number(localStorage.getItem('vehiculoId'));

        if (!isNaN(vehiculoId)) {
          if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
              (position) => {
                console.log(
                  position.coords.latitude,
                  position.coords.longitude
                );
                this.actualizarUbicacion(
                  position.coords.latitude,
                  position.coords.longitude
                );
              },
              (error) => {
                console.error('Error al obtener la ubicación:', error);
              }
            );
          } else {
            console.error(
              'La geolocalización no es compatible con este navegador.'
            );
          }
        }

        this.obtenerPedidosEstado0();
      },

      30000
    ); // Actualizar la ubicación cada 5 minutos (300000 ms)
  }

  marcarPedidoEntregado(pedido: PedidoWithInfo) {
    console.log('Marcar pedido como entregado:', pedido.pedidoId);

    // Llamar al servicio para marcar el pedido como entregado
    this.pedidoService.marcarPedidoComoEntregado(pedido.pedidoId!).then(
      () => {
        console.log('Pedido marcado como entregado exitosamente');
        // Realiza cualquier otra acción que desees después de marcar el pedido como entregado
      },
      (error: any) => {
        console.error('Error al marcar el pedido como entregado:', error);
        // Maneja el error como desees
      }
    );
  }

  private obtenerPedidosEstado0(): void {
    from(this.pedidoService.obtenerPedidos()).subscribe(
      (response: AxiosResponse<Pedido[]>) => {
        const pedidos: Pedido[] = response.data;
        console.log(pedidos);
        this.pedidosPendientes = pedidos.filter(
          (pedido) => pedido.status === EstadoPedido.$0
        );
        

        this.obtenerUbicacionesPedidos(); // Llamar a la función para obtener ubicaciones de pedidos en proceso
      },
      (error: any) => {
        console.error('Error al obtener los pedidos:', error);
        // Aquí puedes mostrar un mensaje de error al usuario si lo deseas.
      }
    );
  }

  obtenerPedidosPorVehiculo() {
    const vehiculoId = localStorage.getItem('vehiculoId');
    this.pedidoService
      .obtenerPedidosPorVehiculo(Number(vehiculoId))
      .then(response => {
        // Filtra los pedidos en proceso
        this.pedidosEnProceso = response.data;
        console.log(this.pedidosEnProceso);
        this.obtenerUbicacionesPedidosEnProceso()
      })
      .catch(error => {
        console.error('Error al obtener los pedidos por vehículo:', error);
        // Aquí puedes mostrar un mensaje de error al usuario si lo deseas.
      });
  }
  private async obtenerUbicacionesPedidosEnProceso(): Promise<void> {
    this.pedidosEnProcesoConInfo = []; // Limpiar el array antes de agregar nuevos elementos
  
    for (const pedido of this.pedidosEnProceso) {
      const pedidosEnProcesoConInfo: PedidoWithInfo = { ...pedido };
      
  
      if (pedido) {
        try {
          const coordenadas = await this.obtenerCoordenadas(pedido.direccionEntrega!);
          const distancia = await this.obtenerDistancia(coordenadas.lat, coordenadas.lon);
          const tiempo = await this.obtenerTiempo(
            coordenadas.lat,
            coordenadas.lon,
            this.latitud,
            this.longitud
          );
  
        
          pedidosEnProcesoConInfo.distancia = distancia;
          pedidosEnProcesoConInfo.tiempo = tiempo;
        } catch (error) {
          console.error('Error al obtener datos:', error);
          // Aquí puedes mostrar un mensaje de error al usuario si lo deseas.
        }
      }
  
      this.pedidosEnProcesoConInfo.push(pedidosEnProcesoConInfo);
    }
  }
  

 

  private async obtenerUbicacionesPedidos(): Promise<void> {
    for (const pedido of this.pedidosPendientes) {
      const pedidoConInfo: PedidoWithInfo = { ...pedido };
      const direccionEntrega = pedido.direccionEntrega;
      if (direccionEntrega) {
        try {
          const coordenadas = await this.obtenerCoordenadas(direccionEntrega);

          const distancia = await this.obtenerDistancia(
            coordenadas.lat,
            coordenadas.lon
          );
          const tiempo = await this.obtenerTiempo(
            coordenadas.lat,
            coordenadas.lon,
            this.latitud,
            this.longitud
          );
          pedidoConInfo.pedidoId = pedido.pedidoId;

          // Almacenar los datos de distancia y tiempo en el objeto de pedido con información adicional
          pedidoConInfo.distancia = distancia;
          pedidoConInfo.tiempo = tiempo;
        } catch (error) {
          console.error('Error al obtener datos:', error);
          // Aquí puedes mostrar un mensaje de error al usuario si lo deseas.
        }
      }
      this.pedidosConInfo.push(pedidoConInfo);
    }
  }

  private async obtenerCoordenadas(direccion: string) {
    console.log('paso por aqui');
    const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(
      direccion
    )}`;
    console.log(url);

    try {
      const response = await axios.get(url);
      const data = response.data;

      if (data && data.length > 0) {
        const result = data[0];
        const lat = parseFloat(result.lat);
        const lon = parseFloat(result.lon);
        return { lat, lon };
      } else {
        throw new Error(
          'No se encontraron coordenadas para la dirección especificada.'
        );
      }
    } catch (error) {
      console.error('Error al obtener las coordenadas:', error);
      throw error;
    }
  }

  private async obtenerDistancia(lat2: number, lon2: number): Promise<number> {
    const vehiculoIdStr = localStorage.getItem('vehiculoId');
    if (vehiculoIdStr !== null) {
      const vehiculo = await this.vehiculoService
        .obtenerVehiculo(parseInt(vehiculoIdStr))
        .toPromise();
      if (vehiculo !== undefined) {
        this.latitud = vehiculo.ubicacionLat;
        this.longitud = vehiculo.ubicacionLon;
      }
      console.log(
        'Las coordenadas que envio son: ',
        this.longitud,
        this.latitud
      );
      const url = `https://router.project-osrm.org/route/v1/driving/${this.longitud},${this.latitud};${lon2},${lat2}`;

      try {
        const response = await fetch(url);
        const data = await response.json();

        // Extraer la distancia en metros
        const distancia = data.routes[0].distance;

        return distancia;
      } catch (error) {
        console.error('Error al obtener la distancia:', error);
        throw error;
      }
    }
    return 1;
  }

  private async obtenerTiempo(
    lat1: number,
    lon1: number,
    lat2: number,
    lon2: number
  ): Promise<{ horas: number; minutos: number; segundos: number }> {
    const url = `https://router.project-osrm.org/route/v1/driving/${this.longitud},${this.latitud};${lon1},${lat1}`;

    try {
      const response = await fetch(url);
      const data = await response.json();

      // Extraer el tiempo en segundos
      const tiempoSegundos = data.routes[0].duration;
      console.log('TIEEEEEMPO', tiempoSegundos);

      // Convertir a horas, minutos y segundos
      const horas = Math.floor(tiempoSegundos / 3600);
      const minutos = Math.floor((tiempoSegundos % 3600) / 60);
      const segundos = Math.floor(tiempoSegundos % 60);

      return { horas, minutos, segundos };
    } catch (error) {
      console.error('Error al obtener el tiempo:', error);
      throw error;
    }
  }
}
