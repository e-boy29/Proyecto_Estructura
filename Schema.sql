create database reporte;
use reporte;

create table clientes(
no_cliente int Primary Key,
nombre_cliente varchar(35) not null,
apellido_cliente varchar(20) not null,
direccion_cliente varchar(35),
telefono_cliente varchar(12) not null,
email_cliente varchar(35) not null,
);

select * from clientes;

create table lista_espera(
tipo_equipo varchar(10) Primary key,
id_equipo varchar(10) not null,
no_cliente int not null,
fecha_entrega date not null,
fecha_regreso date not null,
foreign key (no_cliente) references clientes (no_cliente));

select * from lista_espera;

create table equipo(
id_equipo varchar(10) Primary key,
marca_equipo varchar(20) not null,
modelo_equipo varchar(25) not null,
no_serie varchar(15) not null,
costo varchar(20) not null,
tipo_equipo varchar(10) not null,
disponible varchar(2) not null,
foreign key (tipo_equipo) references lista_espera (tipo_equipo));

select * from equipo;