select * from AspNetUsers;

delete from Usuarios;
USE BibliotecaDB;
GO
EXEC sp_tables @table_owner = 'dbo';