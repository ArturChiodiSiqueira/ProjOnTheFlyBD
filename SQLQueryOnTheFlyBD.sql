create database OnTheFlyBD;

use OnTheFlyBD;

create table Passageiro (
Cpf varchar(11) not null,
Nome varchar(50) not null,
DataNascimento Date not null,
Sexo char(1) not null,
UltimaCompra Date not null,
DataCadastro Date not null,
Situacao char(1) not null
Constraint PK_Passageiro primary key (Cpf)
);

create table CompanhiaAerea (
Cnpj varchar(14) not null,
RazaoSocial varchar(50) not null,
DataAbertura Date not null,
UltimoVoo Date not null,
DataCadastro Date not null,
Situacao char(1) not null
Constraint PK_CompanhiaAerea primary key (Cnpj)
);

create table Aeronave (
Inscricao varchar(6) not null,
Cnpj varchar(14) foreign key references CompanhiaAerea(Cnpj) not null,
Capacidade int not null,
AssentosOcupados varchar(3) not null,
UltimaVenda Date not null,
DataCadastro Date not null,
Situacao varchar(1) not null
Constraint PK_InscricaoAeronave primary key (Inscricao)
);

create table Voo (
ID varchar(5) not null primary key,
Inscricao varchar(6) foreign key references Aeronave(Inscricao) not null,
Destino varchar(3) not null,
DataVoo varchar(20) not null,
DataCadastro Date not null,
AssentosOcupados int not null,
Situacao char(1) not null
);

create table PassagemVoo (
ID varchar(6) not null primary key,
IDVoo varchar(5) foreign key references Voo(ID) not null,
DataUltimaOperacao Date not null,
Valor float not null,
Situacao char(1) not null
);

create table Venda (
ID int identity not null primary key,
Cpf varchar(11) foreign key references Passageiro(Cpf) not null,
DataVenda varchar(50) not null,
TotalVendas varchar(50) not null
);

create table ItemVenda (
ID int identity not null primary key,
IDVenda int identity foreign key references Venda(ID) not null,
IDPassagem varchar(6) foreign key references PassagemVoo(ID) not null,
ValorUnitario float not null
);

create table CadastroBloqueados (
Cnpj varchar(50) not null primary key
);

create table CadastroRestritos (
Cpf varchar(50) not null primary key
);

create table Aeroporto (
Iata varchar(3) not null primary key
);

insert into Aeroporto (Iata) values ('BSB'),('CGH'),('GIG'),('SSA'),('FLN'),('POA'),('VCP'),('REC'),('CWB'),('BEL'),('VIX'),('SDU'),('CGB'),('CGR'),('FOR'),('MCP'),('MGF'),('GYN'),('NVT'),('MAO'),('NAT'),('BPS'),('MCZ'),('PMW'),('SLZ'),('GRU'),('LDB'),('PVH'),('RBR'),('JOI'),('UDI'),('CXJ'),('IGU'),('THE'),('AJU'),('JPA'),('PNZ'),('CNF'),('BVB'),('CPV'),('STM'),('IOS'),('JDO'),('IMP'),('XAP'),('MAB'),('CZS'),('PPB'),('CFB'),('FEN'),('JTC'),('MOC');

select * from Passageiro;
select * from CompanhiaAerea;
select * from Aeronave;
select * from Voo;
select * from Aeroporto;
select * from PassagemVoo;