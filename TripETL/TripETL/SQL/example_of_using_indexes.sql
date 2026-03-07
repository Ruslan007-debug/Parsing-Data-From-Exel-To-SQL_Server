-- Find out which PULocationId has the highest tip_amount on average
SELECT TOP 1 PULocationID, AVG(TipAmount) AS AvgTip
FROM Trips
GROUP BY PULocationID
ORDER BY AvgTip DESC;

-- Find the top 100 longest fares in terms of trip_distance
SELECT TOP 100 PULocationID, TripDistance, FareAmount
FROM Trips
ORDER BY TripDistance DESC;

-- Find the top 100 longest fares in terms of time spent traveling
SELECT TOP 100 PULocationID, PickupDatetime, DropoffDatetime,
    DATEDIFF(MINUTE, PickupDatetime, DropoffDatetime) AS DurationMinutes
FROM Trips
ORDER BY DurationMinutes DESC;