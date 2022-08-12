create database my_finance;
use my_finance;

create table plano_contas(
	id int identity(1,1) not null,
	descricao varchar(50) not null,
	tipo char(1) not null,
	primary key(id)
);

create table transacao(
	id bigint  identity(1,1) not null,
	data datetime not null,
	valor decimal(9,2) not null,
	tipo char(1) not null,
	historico text null,
	id_plano_conta  int not null
	primary key(id),
	foreign key(id_plano_conta) references plano_contas
);

select * from plano_contas;

insert into plano_contas(descricao,tipo) values('Aluguel', 'C');
insert into plano_contas(descricao,tipo) values('Alimnetação', 'D');
insert into plano_contas(descricao,tipo) values('Gasolina', 'D');
insert into plano_contas(descricao,tipo) values('Vigens', 'C');

delete from plano_contas where id=6;
update plano_contas set tipo ='D' where id=1; 

select * from transacao

insert into transacao(data, valor, tipo, historico,id_plano_conta)
values(getdate(),100.47, 'D', 'Gasolina para Viagem', 3);

insert into transacao(data, valor, tipo, historico,id_plano_conta)
values(getdate(),10.47, 'D', 'Almoço', 2);

-- todas as transações menores que 50 pila
select * from transacao where valor<50

select sum(valor) as total from transacao where tipo = 'D'

--consulta conjunta 
select data, valor, t.tipo, historico, p.descricao as 'plano_conta'
from transacao as t inner join plano_contas as p
on p.id = t.id_plano_conta


