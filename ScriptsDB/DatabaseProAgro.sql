create database ProAgroDB
use ProAgroDB

create schema pgo

create table pgo.Georreferencias(
IdGeorreferencia int primary key identity(1,1) not null,
IdEstado int not null,
Latitud float not null,
Longitud float not null
)

create table pgo.Usuario(
IdUsuario int primary key identity(1,1) not null,
Contraseña BINARY(64) not null,
Nombre varchar(100) not null,
FechaNacimiento datetime not null,
RFC varchar(13) not null
)
create table pgo.Estado(
IdEstado int primary key identity(1,1) not null,
Estado nvarchar(100) not null,
Abreviatura nvarchar(10)  not null
)

create table pgo.Permisos(
IdUsuario int references pgo.Usuario(IdUsuario)
on delete cascade not null,
IdEstado int references pgo.Estado(IdEstado)
)
