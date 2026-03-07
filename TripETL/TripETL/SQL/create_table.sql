
CREATE DATABASE TripDB;
GO

USE TripsDB;
GO

CREATE TABLE Trips
(
    Id INT IDENTITY PRIMARY KEY,

    PickupDatetime DATETIME2 NOT NULL,

    DropoffDatetime DATETIME2 NOT NULL,

    PassengerCount SMALLINT NULL,

    TripDistance FLOAT NOT NULL,

    StoreAndForwardFlag VARCHAR(3) NULL,

    PULocationID INT NOT NULL,

    DOLocationID INT NOT NULL,

    FareAmount DECIMAL(10,2) NOT NULL,

    TipAmount DECIMAL(10,2) NULL
);
GO

CREATE INDEX IX_Trips_PULocation_Tip
ON Trips(PULocationId)
INCLUDE (TipAmount);
GO

CREATE INDEX IX_Trips_Distance
ON Trips(TripDistance DESC);
GO

CREATE INDEX IX_Trips_Time
ON Trips(PickupDatetime, DropoffDatetime);
GO