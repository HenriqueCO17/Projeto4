-- Criando a database
create database dbServer;

-- Usando a database

Use dbServer;

-- Criando a tabela Usuario
Create table Usuario
(Id int primary key auto_increment,
Nome varchar(200),
Email varchar(200),
Senha varchar(200) 
);

-- Criando a tabela Produto 
Create table Produto
(Id int primary key auto_increment,
Nome varchar(200),
Descricao varchar(200),
Preco decimal(6,2),
Quantidade int  
);

insert into Usuario (Nome,Email,Senha) values ('Dangar','admin@gmail.com','12345');

select * from Produtos;