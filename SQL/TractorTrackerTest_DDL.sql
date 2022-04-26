use master;
GO
drop database if exists TractorTrackerTest;
GO
create database TractorTrackerTest;
GO
use TractorTrackerTest;
GO

create table Tractor (
	tractorId int primary key identity(1,1),
	tractorName varchar(150) not null,
	tractorOwner varchar(75) not null
);

create table Driver (
	driverId int primary key identity(1,1),
	driverName varchar(75) not null,
	driverHometown varchar(100)
);

create table Pull (
	pullId int primary key identity(1,1),
	pullName varchar(100) not null,
	pullPromoter varchar(75) not null,
	pullCity varchar(150) not null,
	pullState varchar(2) not null
);

create table TractorDriver (
	tractorId int not null,
	driverId int not null,
	constraint fk_TractorDriver_tractorId
		foreign key (tractorId)
		references Tractor(tractorId),
	constraint fk_TractorDriver_driverId
		foreign key (driverId)
		references Driver(driverId)
);

create table PullTractorDriver (
	pullId int not null,
	tractorId int not null,
	driverId int not null,
	placement int,
	isDisqualified bit not null default 0,
		constraint pk_PullTractorDriver
		primary key (pullId, tractorId),
	constraint fk_PullTractorDriver_tractorId
		foreign key (tractorId)
		references Tractor(tractorId),
	constraint fk_PullTractorDriver_driverId
		foreign key (driverId)
		references Driver(driverId),
	constraint fk_PullTractorDriver_pullId
		foreign key (pullId)
		references Pull(pullId),
	constraint uq_PullTractorDriver_pullId_tractorId
		unique (pullId, tractorId)
);
	
use master;
GO