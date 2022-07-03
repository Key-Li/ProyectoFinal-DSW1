--/*BASE DE DATOS*/
USE MASTER
DROP DATABASE IF EXISTS XAVIER_INSTITUTE;
CREATE DATABASE XAVIER_INSTITUTE;
USE XAVIER_INSTITUTE;


  create table tb_alumnos(
  idalumno int IDENTITY(40001,1) not null primary key,
  nomalum varchar (45) not null,
  apealum varchar (45) not null,
  dnialum char (8) not null unique,
  fechalum char (10) not null,
  celalum char(9) not null,
  usualum varchar(45) not null unique,
  passalum varchar(6) not null
  );
 
  create table tb_horarios (
  idhorario int primary key,
  deshorario varchar(15) not null
);

create table tb_profesores(
  idprof int IDENTITY(30001,1) not null primary key,
  nomprof varchar (45) not null,
  apeprof varchar (45) not null,
  espeprof varchar (45) not null,
  dniprof char (8) unique not null,
  celprof char(9) not null
  );
 
  create table tb_cursos(
  idcurso int IDENTITY(20001,1) not null primary key,
  nomcurso varchar (45) not null,
  idhorario int references tb_horarios,
  idprof int references tb_profesores
   );
   
  create table tb_matriculas(
  idmat int  IDENTITY(90001,1) not null primary key,
  fechmat char(10) not null,
  idalumno int not null references tb_alumnos,
  idcurso int not null references tb_cursos,
  idhorario int references tb_horarios
  );

  
insert into tb_horarios values (1,'Mañana');
insert into tb_horarios values (2,'Tarde');
insert into tb_horarios values (3,'Noche');

insert into tb_profesores values ('Charles','Xavier','Telequinesis','20632580','123456789');
insert into tb_profesores values ('Elizabeth','Braddock','Psicoquinesis','43635582','123456789');
insert into tb_profesores values ('Erik','Lensherr','Magnetismo','42632589','123456789');
insert into tb_profesores values ('Raven','Darkhölme','Mimetismo','41632538','123456789');

insert into tb_alumnos values ('Anna Marie','D Ancanto','56325961','2004-10-01','923456739','rogue@xmen.com','111111');
insert into tb_alumnos values ('Bobby','Drake','58320963','2003-02-11','998456210','iceman@xmen.com','222222');
insert into tb_alumnos values ('Pietro','Maximoff','58320970','2003-03-01','998456233','quicksilver@xmen.com','333333');

insert into tb_cursos values ('Telequinesis',1,30001);
insert into tb_cursos values ('Psicoquinesis',2,30002);
insert into tb_cursos values ('Magnetismo',1,30003);
insert into tb_cursos values ('Mimetismo',3,30004);

select*from tb_cursos;
select*from tb_matriculas;
select*from tb_alumnos;
select*from tb_profesores;
select*from tb_horarios;


create proc SP_ValidarLogueoAlu
@usu char(45), 
@pass char(8)
as
select * from tb_alumnos where usualum = @usu and passalum = @pass;
go

create proc SP_AlumnoRegistro
@nomalum varchar(45),
@apealum varchar(45),
@dnialum char(8),
@fechalum date,
@celalum char(9),
@usualum varchar(45),
@passalum varchar(45)
as
Insert tb_alumnos
values(@nomalum,@apealum,@dnialum,@fechalum,@celalum,@usualum,@passalum)
go

create proc SP_AlumnoList
as
select * from tb_alumnos;
go

create proc SP_AlumnoActualiza
@idalumno int,
@nomalum varchar(45),
@apealum varchar(45),
@dnialum char(8),
@fechalum date,
@celalum char(9),
@usualum varchar(45),
@passalum varchar(45)
as
update tb_alumnos set 
nomalum=@nomalum, apealum=@apealum, dnialum=@dnialum,fechalum=@fechalum, celalum=@celalum, usualum=@usualum, passalum=@passalum
where idalumno = @idalumno
go

create proc SP_ProfesorList
as
select * from tb_profesores;
go

create proc SP_CursoList
as
select c.idcurso,c.nomcurso,c.idhorario,h.deshorario,c.idprof,(p.nomprof+' '+p.apeprof)as nombre from tb_cursos c
inner join tb_horarios h on c.idhorario=h.idhorario inner join tb_profesores p
on c.idprof=p.idprof;
go

create proc SP_MatricualRegistro
@fechmat date,
@idalumno int,
@idcurso int,
@idhorario int
as
Insert tb_matriculas
values(@fechmat,@idalumno,@idcurso,@idhorario)
go

create proc SP_ValidarMatricula
@idalumno int, 
@idcurso int
as
select * from tb_matriculas where idalumno=@idalumno and idcurso =@idcurso;
go

create proc SP_MatriculaList
@idalumno int
as
select m.idmat,m.fechmat,c.nomcurso,(p.nomprof+' '+p.apeprof)as nombre,h.deshorario
from tb_matriculas m
inner join tb_horarios h on m.idhorario=h.idhorario inner join tb_cursos c
on m.idcurso=c.idcurso inner join tb_profesores p on c.idprof=p.idprof
where idalumno=@idalumno;
go

