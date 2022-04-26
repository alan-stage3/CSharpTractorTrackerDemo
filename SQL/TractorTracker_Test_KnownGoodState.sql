use TractorTrackerTest;
GO

DROP PROCEDURE IF EXISTS [SetKnownGoodState];  
GO  

CREATE PROCEDURE [SetKnownGoodState]
AS
BEGIN
    DELETE FROM PullTractorDriver;
    DELETE FROM TractorDriver;
    DELETE FROM Pull;
    DELETE FROM Driver;
    DELETE FROM Tractor;

    DBCC CHECKIDENT('Tractor', RESEED, 0);
    DBCC CHECKIDENT('Driver', RESEED, 0);
    DBCC CHECKIDENT('Pull', RESEED, 0);

    
    insert into Tractor (tractorName, tractorOwner)
	    values
    	('The Crop Doctor TEST', 'Jon Silsby'),
	    ('Tinker Toy TEST', 'Don Masterson');

    insert into Driver (driverName, driverHometown)
	    values
	    ('Jon Silsby TEST', 'Detroit, MI'),
	    ('Kevin Masterson', 'St. Louis, MO')

insert into Pull (pullName, pullPromoter, pullCity, pullState)
	values
	('Brandenburg County Fair TEST', 'Meade County Fair Board', 'Brandenburg', 'KY'),
	('National Farm Machinery Show TEST', 'NTPA', 'Louisville', 'KY'),
	('Mid-America Trucking Show TEST', 'PPL', 'Louisville', 'KY');


insert into TractorDriver (tractorId, driverId)
	values
	(1,1),
	(2,2),
	(1,2),
	(2,1);

insert into PullTractorDriver (pullId, tractorId, driverId, placement, isDisqualified)
	values
	(1,1,1,1,0),
	(1,2,1,3,0),
	(2,2,2,2,0),
	(2,1,2,NULL,1);

END;
