## Correr un SqlServer local con Docker

docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=S3cr3tPa$$word' -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

## Crear tabla GenID para guardar los IDs de las tablas

CREATE TABLE GenID (
    name VARCHAR(50) NOT NULL,
    id INT NOT NULL,
    departamento INT NOT NULL,
);

## Insertar el registro de prueba

insert into GenID (name, id, departamento) values ('TODOS', 0, 11);