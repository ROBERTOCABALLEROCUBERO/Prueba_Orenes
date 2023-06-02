# PRUEBA_ORENES_ROBERTO_CABALLERO

Prueba sobre el servicio de reparto.

## Método de ejecución

1. Usa Visual Studio para el proyecto .NET y VSCode para la aplicación en Angular.
2. Crea una base de datos.
3. Modifica el archivo de configuración y la cadena de conexión.
4. Ejecuta las migraciones o crea tu propia migración y actualiza la base de datos.
5. En Angular, dentro de la ruta `.\angular-Pruebatecnica\src\app\API\services`, modifica los archivos con la URL de la API correspondiente. En mi caso, es `https://localhost:7191/`.
6. Ejecuta el proyecto en ambos entornos. Asegúrate de tener las herramientas necesarias instaladas.

## Nivel 1: Funcionamiento básico de CRUD

La aplicación se divide en 4 capas:
- Modelo
- Repository
- Services
- Controller

Se ha implementado un CRUD sobre el modelo que consta de las siguientes entidades:
- Cliente
- Pedido
- PedidoEntregado
- Ubicacion
- Vehiculo

También se ha incluido pruebas (testing).

## Nivel 2: Descripción de funcionalidades adicionales

En esta parte, se ha añadido la funcionalidad de que un vehículo pueda seleccionar un pedido como "EnProceso" a través de la aplicación de Angular. Esto indica que se empiezan a guardar las ubicaciones del vehículo en el pedido correspondiente. 

La ubicación del vehículo se adquiere a través del frontend y mediante una API externa de rutas se obtiene el tiempo estimado, el cual se va actualizando y registrando en tiempo real. 

Para obtener la ruta y el tiempo estimado, se utilizan los servicios de `https://nominatim.openstreetmap.org` y `https://router.project-osrm.org/`. 

A través de llamadas periódicas a la API, se obtiene la última ubicación del vehículo prácticamente en tiempo real, además de actualizar el tiempo restante del pedido.

La aplicación también se ha asegurado, de modo que cada vez que se realiza una petición de inicio de sesión, se genera un token que se guarda en el almacenamiento local (localStorage).

Es recomendable utilizar [Swagger](https://localhost:7191/swagger/index.html) para ejecutar la aplicación.

## Partes a mejorar

Aunque se han priorizado otros aspectos de la aplicación, una de las áreas que se puede mejorar es la falta de roles en la aplicación.
