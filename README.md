# PruebaMultimoney-CarlosAguilar

## Requisitos
.NET 9
Sql Server (La prueba se realizó con local db)
Postman para realizar pruebas del api

## Pasos a realizar base de datos (PASO 1)
Ejecutar el script 01-create-tables.sql (En este se crea la bd y tablas)
Ejecutar el script 02-create-stored-procedures.sql (En este se crean store procedure)
Ejecutar el script 03-seed-data.sql (En este se registran tipos de transacciones y cuentas para pruebas)

## Pasos a realizar solución .net (PASO 2)
Ejecutar un git clone a este repositorio
Deste la terminal en la carpeta del proyecto ejecutar los siguientes comandos
dotnet build
dotnet run

## Pasos a realizar solución .net (PASO 3)
Por defecto la aplicación se levantará en la ruta: http://localhost:5136
Cargar la colección de postman llamada pruebaMultimoney (Esta ya lleva los datos para realizar pruebas)

##Validar que las funciones estén correctas.
Se agregó log y middleware para manejo de excepciones globales como bonus
