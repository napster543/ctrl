use developer00
go
IF OBJECT_ID('eventos', 'U') IS NOT NULL  
  DROP TABLE eventos; 

CREATE TABLE eventos(
	id int identity(1,1) primary key not null,
	Nombre_Institucion_Superior varchar(80),
	Direccion_Institucion varchar(80),
	Numero_Alumno int,
	Hora_inicio time,
	Valor_Servicio money
)	

/* ****************************************************** */

EXEC RegistroEvento 'Institucion', 'direccion', 5, '05:15', 200000

IF OBJECT_ID('RegistroEvento', 'P') IS NOT NULL  
  DROP PROCEDURE RegistroEvento;
GO
CREATE PROCEDURE RegistroEvento(
	@Id int = 0,
	@Nombre_Institucion varchar(80),
	@Direccion_Institucion varchar(80),
	@Numero_Alumno int = 0,
	@Hora_inicio time = null,
	@Valor_Servicio money = null
)
AS
if (@Id = 0)
	INSERT INTO eventos(Nombre_Institucion_Superior, Direccion_Institucion, Numero_Alumno, Hora_inicio, Valor_Servicio)
			VALUES (@Nombre_Institucion, @Direccion_Institucion, @Numero_Alumno, @Hora_inicio, @Valor_Servicio)
ELSE
	UPDATE eventos SET Nombre_Institucion_Superior = @Nombre_Institucion, 
				Direccion_Institucion = @Direccion_Institucion,
				Numero_Alumno = @Numero_Alumno,
				Hora_inicio = @Hora_inicio,
				Valor_Servicio = @Valor_Servicio
	where id = @Id


/* ****************************************************** */
exec ListarEventos ''
IF OBJECT_ID('ListarEventos', 'P') IS NOT NULL  
  DROP PROCEDURE ListarEventos;
GO
CREATE PROCEDURE ListarEventos(
	@Nombre_Institucion varchar(40)
)
as
SELECT id, Nombre_Institucion_Superior, Direccion_Institucion, Numero_Alumno, Hora_inicio, Valor_Servicio FROM eventos
 WHERE Nombre_Institucion_Superior like '%' + @Nombre_Institucion + '%' OR @Nombre_Institucion = ''

 /*******************************************************************************/
 exec ObtenerListarEventos 1

 IF OBJECT_ID('ObtenerListarEventos', 'P') IS NOT NULL  
  DROP PROCEDURE ObtenerListarEventos;
GO
CREATE PROCEDURE ObtenerListarEventos(
	@IdEvento int
)
as
SELECT id, Nombre_Institucion_Superior, Direccion_Institucion, Numero_Alumno, Hora_inicio, Valor_Servicio FROM eventos
 WHERE id = @IdEvento

 /*******************************************************************************/
  IF OBJECT_ID('EliminarrEventos', 'P') IS NOT NULL  
  DROP PROCEDURE EliminarrEventos;
GO
CREATE PROCEDURE EliminarrEventos(
	@IdEvento int
)
as
delete from eventos
 WHERE id = @IdEvento