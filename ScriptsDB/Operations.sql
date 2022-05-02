create procedure Pgo.GetGeorreferencesByUserId @UserId int 
as
begin
select s.Latitud, s.Longitud from pgo.Georreferencias s
inner join pgo.Permisos p on p.IdUsuario = @UserId
where s.IdEstado = p.IdEstado
end
go
create procedure Pgo.LoginByCredentials 
@Username nvarchar(100),
@Pass varchar(100)
as
begin
select cast(u.IdUsuario as varchar(100)) IdUsuario, u.Nombre Nombre from pgo.Usuario u
where @Username = u.Nombre and HASHBYTES('sha2_512',@Pass) = u.Contraseña
end
go 
create procedure Pgo.GetGeorreferencesByUserId @UserId int, 
as
begin
select gr.latitud, gr.longitud from pgo.Permisos p 
inner join pgo.Usuario u on u.IdUsuario = p.IdUsuario
inner join pgo.Georreferencias gr on gr.IdEstado = p.IdEstado
where @UserId = u.IdUsuario
end


go
create procedure pgo.ChangeNameByUserId @UserId int, @Username nvarchar(100)
as
begin
update pgo.Usuario set nombre = @Username where IdUsuario = @UserId
select IdUsuario, Nombre from pgo.Usuario where IdUsuario = @UserId
end
