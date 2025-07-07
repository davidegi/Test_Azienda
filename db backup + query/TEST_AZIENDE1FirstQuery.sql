use TEST_AZIENDE1
go 
--DROP TABLE IF EXISTS Ruolo
--DROP TABLE IF EXISTS Azienda
--DROP TABLE IF EXISTS Utente
--DROP TABLE IF EXISTS Dipendente
--DROP TABLE IF EXISTS DipendenteAnagrafica
--DROP TABLE IF EXISTS HistoryLog

create table Ruolo(
IDRuolo int not null identity(1, 1) primary key,
Tipo varchar(250),
Descrizione varchar(250)
);

create table Azienda (
IDAzienda int not null identity(1, 1) primary key,
CodiceFiscale varchar(16) null,
PartitaIva varchar(13) null,
Descrizione varchar(250) not null,
Capitale decimal(18,2),
IDRuolo int not null,
DataInizioAtt date not null,
DataFineAtt date,
constraint FK_Azienda_Ruolo foreign key (IDRuolo)
references Ruolo (IDRuolo),
-- Vincolo 'CHECK' per richiedere almeno uno dei due
constraint CK_CodiceFiscaleOrPartitaIva check(
CodiceFiscale IS NOT NULL OR PartitaIva IS NOT NULL)
);

create table Utente(
IDUtente  int not null identity(1, 1) primary key,
Username varchar(50) not null,
Email varchar(250) not null,
[Password] varchar(250) not null,
IDRuolo int not null,
constraint FK_Utente_Ruolo foreign key (IDRuolo)
references Ruolo (IDRuolo)
);

create table Dipendente(
IDDipendente int not null identity(1, 1) primary key,
CodiceFiscale varchar(16) not null,
PartitaIva varchar(13),
Iban varchar(50) not null,
RAL decimal(12, 2),
IDAzienda int not null,
Professione varchar(250),
Qualifica varchar(250),
DataAssunzione date not null,
DataFineAssunzione date,
constraint FK_Azienda_Dipendente foreign key (IDAzienda)
references Azienda (IDAzienda)
);

create table DipendenteAnagrafica(
IDDipendenteAnag int not null identity(1, 1) primary key,
nome varchar(250) not null,
cognome varchar(250) not null,
LuogoNascita varchar(250) not null,
DataNascita date not null,
DataDecesso date,
Indirizzo varchar(250),
CAP int,
Sesso varchar(1),
);

create table HistoryLog(
Descrizione varchar(250) not null,
Chiave varchar(250) not null,
DataLog date not null
);

-- Inserimento dati nella tabella Ruolo
INSERT INTO Ruolo (Tipo, Descrizione) VALUES
('az' , 'Amministratore'),
('az' , 'filiale'),
('az' , 'startup'),
('ut' , 'Amministratore'),
('ut' , 'Manager'),
('ut' , 'Lettura');

-- Inserimento dati nella tabella Azienda
INSERT INTO Azienda (CodiceFiscale, PartitaIva, Descrizione, Capitale, IDRuolo, DataInizioAtt, DataFineAtt) VALUES
('12345678901', 'IT12345678901', 'Azienda Alpha', 1000000.00, 1, '2000-01-01', NULL),
('23456789012', 'IT23456789012', 'Azienda Beta', 2000000.00, 2, '2005-05-15', NULL),
('34567890123', 'IT34567890123', 'Azienda Gamma', 1500000.00, 3, '2010-10-10', NULL),
('45678901234', 'IT45678901234', 'Azienda Delta', 2500000.00, 4, '2015-03-20', NULL),
('56789012345', 'IT56789012345', 'Azienda Epsilon', 3000000.00, 5, '2020-07-25', NULL),
('67890123456', 'IT67890123456', 'Azienda Zeta', 3500000.00, 6, '2022-11-30', NULL);

-- Inserimento dati nella tabella Utente
INSERT INTO Utente (Username, Email, [Password], IDRuolo) VALUES
('user1', 'user1@example.com', 'password1', 1),
('user2', 'user2@example.com', 'password2', 2),
('user3', 'user3@example.com', 'password3', 3),
('user4', 'user4@example.com', 'password4', 4),
('user5', 'user5@example.com', 'password5', 5),
('user6', 'user6@example.com', 'password6', 6);

-- Inserimento dati nella tabella Dipendente
INSERT INTO Dipendente (CodiceFiscale, PartitaIva, Iban, RAL, IDAzienda, Professione, Qualifica, DataAssunzione, DataFineAssunzione) VALUES
('CF1234567890A', 'IT12345678901', 'IT60X0542811101000000123456', 30000.00, 1, 'Ingegnere', 'Senior', '2010-01-01', NULL),
('CF2345678901B', 'IT23456789012', 'IT60X0542811101000000123457', 40000.00, 2, 'Analista', 'Junior', '2015-05-15', NULL),
('CF3456789012C', 'IT34567890123', 'IT60X0542811101000000123458', 50000.00, 3, 'Manager', 'Middle', '2020-10-10', NULL),
('CF4567890123D', 'IT45678901234', 'IT60X0542811101000000123459', 60000.00, 4, 'Consulente', 'Senior', '2021-03-20', NULL),
('CF5678901234E', 'IT56789012345', 'IT60X0542811101000000123460', 70000.00, 5, 'Tecnico', 'Junior', '2022-07-25', NULL),
('CF6789012345F', 'IT67890123456', 'IT60X0542811101000000123461', 80000.00, 6, 'Stagista', 'Intern', '2023-11-30', NULL);

-- Inserimento dati nella tabella DipendenteAnagrafica
INSERT INTO DipendenteAnagrafica (nome, cognome, LuogoNascita, DataNascita, DataDecesso, Indirizzo, CAP, Sesso) VALUES
('Mario', 'Rossi', 'Roma', '1980-01-01', NULL, 'Via Roma 1', 100, 'M'),
('Luigi', 'Verdi', 'Milano', '1985-05-15', NULL, 'Via Milano 2', 20100, 'M'),
('Giulia', 'Bianchi', 'Napoli', '1990-10-10', NULL, 'Via Napoli 3', 80100, 'F'),
('Anna', 'Neri', 'Torino', '1995-03-20', NULL, 'Via Torino 4', 10100, 'F'),
('Marco', 'Gialli', 'Firenze', '2000-07-25', NULL, 'Via Firenze 5', 50100, 'M'),
('Sara', 'Blu', 'Bologna', '2005-11-30', NULL, 'Via Bologna 6', 40100, 'F');