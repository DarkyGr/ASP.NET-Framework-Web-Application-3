-- Renta de carros
-- Autor: Guillermo Riaño Melchor

CREATE DATABASE RentaCarros
GO
USE RentaCarros

CREATE TABLE Direcciones(
	DireccionId int identity(1,1) primary key,
	Calle varchar(100) not null,
	Numero int,
	Colonia varchar(100) not null,
	CP int not null,
	Municipio varchar(100),
	Ciudad varchar(100) not null,
	Estado varchar(50) not null
)

CREATE TABLE Empleados(
	EmpleadoId int identity(1,1) primary key,
	DireccionId int not null,
	Nombre varchar(50) not null,
	ApellidoP varchar(50) not null,
	ApellidoM varchar(50) not null,
	Salario float not null,
	Puesto varchar(50) not null
	FOREIGN KEY (DireccionId) REFERENCES Direcciones(DireccionId)
)

CREATE TABLE Clientes(
	ClienteId int identity(1,1) primary key,
	DireccionId int not null,
	Nombre varchar(50) not null,
	ApellidoP varchar(50) not null,
	ApellidoM varchar(50) not null,
	Telefono varchar(10),
	NumLicencia int not null,
	FechaVencimientoLicencia datetime,
	FOREIGN KEY (DireccionId) REFERENCES Direcciones(DireccionId)
)

CREATE TABLE Vehiculos(
	VehiculoId int identity(1,1) primary key,
	Matricula varchar(7) not null,
	Marca varchar(25) not null,
	Modelo varchar(25) not null,
	Capacidad int not null,
	Kilometraje float not null
)

CREATE TABLE Mantenimientos(
	MantenimientoId int identity(1,1) primary key,
	VehiculoId int not null,
	Nota varchar(150) not null,
	Fecha datetime not null,
	FOREIGN KEY (VehiculoId) REFERENCES Vehiculos(VehiculoId)
)

CREATE TABLE Rentas(
	RentaId int identity(1,1) primary key,
	VehiculoId int not null,
	ClienteId int not null,
	EmpleadoId int not null,
	Costo float not null,	
	FechaRenta datetime not null,
	FechaRentaFin datetime not null,
	FOREIGN KEY (VehiculoId) REFERENCES Vehiculos(VehiculoId),
	FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId),
	FOREIGN KEY (EmpleadoId) REFERENCES Empleados(EmpleadoId)
)

-- ----------------------------------------------------------
INSERT INTO Direcciones(Calle, Numero, Colonia, CP, Municipio, Ciudad, Estado)
VALUES ('Morelos', 10, 'Linda Vista', 11111, 'Sancho', 'Puebla', 'Puebla'),
	('Hidalgo', 21, 'Union', 23222, 'San Martin', 'Madero', 'Mexico'),
	('Pavon', 13, 'San Felipe', 75441, 'Coyacotl', 'Tlaxiaco', 'Oaxaca');

INSERT INTO Empleados(DireccionId, Nombre, ApellidoP, ApellidoM, Salario, Puesto)
VALUES (1, 'Carlitos', 'Perez', 'Gomez', 50.15, 'Recepcionista1'),
	(2, 'Paco', 'Memo', 'Ochoa', 150.15, 'Recepcionista2'),
	(3, 'Santiago', 'Sanchez', 'Pinolo', 520.15, 'Recepcionista3');

INSERT INTO Clientes(DireccionId, Nombre, ApellidoP, ApellidoM, Telefono, NumLicencia, FechaVencimientoLicencia)
VALUES (1, 'Carlos', 'Medrano', 'Gomez', '111111111', 63627, '2010-01-01'),
	(2, 'Ulises', 'Sanchez', 'Ochoa', '1112223333', 737282, '2010-01-03'),
	(3, 'Eric', 'Cruz', 'Pinolo', '3323224242', 384734, '2010-01-02');

INSERT INTO Vehiculos(Matricula, Marca, Modelo, Capacidad, Kilometraje)
VALUES ('HD73HD', 'Marca1', 'Modelo1', 4, 100.5),
	('7YEHS', 'Marca2', 'Modelo2', 4, 400.5),
	('S1E2S', 'Marca3', 'Modelo3', 4, 700.5);

INSERT INTO Mantenimientos(VehiculoId, Nota, Fecha)
VALUES (1, 'Cambio de aceite', '2011-01-03'),
	(2, 'Motor arreglado', '2011-02-03'),
	(3, 'Cambio de rines', '2011-03-03');

INSERT INTO Rentas(VehiculoId, ClienteId, EmpleadoId, Costo, FechaRenta, FechaRentaFin)
VALUES (1, 1, 1, 100.5, '2015-01-01', '2015-01-04'),
	(1, 1, 1, 100.5, '2023-02-01', '2023-01-04'),
	(1, 1, 1, 100.5, '2023-01-01', '2023-01-09');


-- ********** VIEWS *************
create view View_Empleados
as
select e.EmpleadoId, 
e.DireccionId, CONCAT('Calle ', direc.Calle, ' #', direc.Numero, ' Col. ', direc.Colonia, ' C,P. ', direc.CP) as Dirección,
e.Nombre,
e.ApellidoP,
e.ApellidoM,
e.Salario,
e.Puesto
from Empleados as e
inner join Direcciones as direc on direc.DireccionId = e.DireccionId

create view View_Clientes
as
select c.ClienteId, 
c.DireccionId, CONCAT('Calle ', direc.Calle, ' #', direc.Numero, ' Col. ', direc.Colonia, ' C,P. ', direc.CP) as Dirección,
c.Nombre,
c.ApellidoP,
c.ApellidoM,
c.Telefono,
c.NumLicencia,
c.FechaVencimientoLicencia
from Clientes as c
inner join Direcciones as direc on direc.DireccionId = c.DireccionId

create view View_Mantenimientos
as
select m.MantenimientoId,
m.VehiculoId, CONCAT('Modelo: ', vehi.Modelo, ' Marca: ', vehi.Marca, ' Matrícula: ', vehi.Matricula) as Vehiculo,
m.Nota,
m.Fecha
from Mantenimientos as m
inner join Vehiculos as vehi on vehi.VehiculoId = m.VehiculoId

create view View_Rutas
as
select r.RentaId,
r.VehiculoId, CONCAT('Modelo: ', vehi.Modelo, ' Marca: ', vehi.Marca, ' Matrícula: ', vehi.Matricula) as Vehiculo,
r.ClienteId, CONCAT(cli.Nombre, ' ', cli.ApellidoP, ' ', cli.ApellidoM) as Cliente_Nombre,
r.EmpleadoId, CONCAT(emp.Nombre, ' ', emp.ApellidoP, ' ', emp.ApellidoM) as Empleado_Nombre,
r.Costo,
r.FechaRenta,
r.FechaRentaFin
from Rentas as r
inner join Vehiculos as vehi on vehi.VehiculoId = r.VehiculoId
inner join Clientes as cli on cli.ClienteId = r.ClienteId
inner join Empleados as emp on emp.EmpleadoId = r.EmpleadoId