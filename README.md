# Catedra-3-Backend
## Proyecto para manejar en un entorno de ventas de productos.
## Para levantar el proyecto se deben seguir lo siguientes pasos:

## Requisitos previos:
- .NET 8.0
- Visual Studio Code 1.95.3 o superior

## Instalación
1.- Primero debemos abrir la consola de comandos apretando las siguientes teclas y escribir 'cmd':

- "Windows + R" y escribimos 'cmd'

2.- Ahora debemos crear una carpeta en donde guardar el proyecto, esta carpeta puede estar donde desee el usuario:
```bash
mkdir [NombreDeCarpeta]
```
3.- Accedemoss a la carpeta.
```bash
cd NombreDeCarpeta
```
4.- Se debe clonar el repositorio en el lugar deseado por el usuario con el siguiente comando:
```bash
git clone https://github.com/AlbertoLyons/Catedra-3-Backend.git
```
5.- Accedemos a la carpeta creada por el repositorio:
```bash
cd Catedra-3-Backend
```
6.- Ahora debemos restaurar las dependencias del proyecto con el siguiente comando:
```bash
dotnet restore
```
7.- Con las dependencias restauradas, abrimos el editor:
```bash
code .
```
8.- Finalmente ya en el editor ejecutamos el siguiente comando para ejecutar el proyecto:
```bash
dotnet run
```
## Estructura del repositorio
- Funciona con una API de tipo REST
- Se utiliza el Framework .NET de C#
- Incluye Identity de Microsoft para la gestión de roles de usuarios
- Utiliza endpoints para realizar el CRUD de la base de datos
- Se utiliza la ruta "http://localhost:5296" para realizar las peticiones HTTP