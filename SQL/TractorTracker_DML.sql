use TractorTracker;

GO

insert into Tractor (tractorName, tractorOwner)
	values
	('The Crop Doctor', 'Jon Silsby'),
	('Bullheaded Binder', 'The Rock'),
	('River Rat', 'Kevin Masterson'),
	('Tinker Toy', 'Don Masterson'),
	('The Kentuckian', 'Wayne Sullivan'),
	('My Wife''s Hawaiian Vacation', 'Randy Simmons'),
	('Hillbilly Rocket', 'Melvin Abbot'),
	('Warpath', 'Simon Simons'),
	('Comman-Deere', 'Doc Riley'),
	('The Commander', 'Jason Hootman'),
	('Wolverine Deere', 'Tony Sisema'),
	('Spare Parts', 'Kevin Heinlen'),
	('My Last Excuse', 'Roger Stein');

insert into Driver (driverName, driverHometown)
	values
	('Jon Silsby', 'Detroit, MI'),
	('The Rock', 'Parts Unknown'),
	('Kevin Masterson', 'St. Louis, MO'),
	('Don Masterson', 'St. Louis, MO'),
	('Wayne Sullivan', 'Warsaw, KY'),
	('Donnie Sullivan', 'Warsaw, KY'),
	('Randy Simmons', 'Possum Trot, KY'),
	('Melvin Abbot', 'La Grange, KY'),
	('Simon Simons', 'Rockwell, IA'),
	('Doc Riley', 'Indianapolis, IN'),
	('Jason Hootman', 'Buffalo, NY'),
	('Tony Sisema', 'Grand Rapids, MI'),
	('Kevin Heinlen', 'Kalamazoo, MI'),
	('Roger Stein', 'Possum Trot, KY'),
	('Mark Filiatreu', 'Cox''s Creek, KY'),
	('Hambone', 'Bardstown, KY');

insert into Pull (pullName, pullPromoter, pullCity, pullState)
	values
	('Brandenburg County Fair', 'Meade County Fair Board', 'Brandenburg', 'KY'),
	('National Farm Machinery Show', 'NTPA', 'Louisville', 'KY'),
	('Mid-America Trucking Show', 'PPL', 'Louisville', 'KY');

insert into TractorDriver (tractorId, driverId)
	values
	(1,1),
	(2,2),
	(3,3),
	(4,4),
	(5,5),
	(6,6),
	(7,7),
	(8,8),
	(9,12),
	(10,9),
	(11,2),
	(12,14);

insert into PullTractorDriver (pullId, tractorId, driverId, placement, isDisqualified)
	values
	(1,1,1,1,0),
	(1,2,1,3,0),
	(1,3,1,2,0),
	(1,4,1,NULL,1),
	(2, 8,2,1,0),
	(2, 2,2,3,0),
	(2, 4,2,2,0),
	(2, 7,2,NULL,1),
	(3, 3,3,3,0),
	(3, 2,3,2,0),
	(3, 6,3,1,0),
	(3, 8,3,NULL,1);


use master;
GO
