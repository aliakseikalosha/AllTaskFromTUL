-- 1 kolik krat pujcovali auto
/*
SELECT [user_name], X.pocet
FROM [User] U LEFT JOIN ( 
    SELECT [user_id],count([user_id]) as pocet
    FROM [Reservation]
    GROUP BY [user_id]
) AS X
ON X.[user_id] = U.[user_id]

SELECT u.[user_name],COUNT(r.[user_id]) 
FROM Reservation r RIGHT JOIN [User] u on r.[user_id] = u.[user_id]
GROUP BY r.[user_id],u.[user_name]
*/
-- 2 kdo nikdy nepujcoval auto
/*
SELECT [user_name]
FROM (
    SELECT [user_name], X.pocet
    FROM [User] U LEFT JOIN ( 
        SELECT [user_id],count([user_id]) as pocet
        FROM [Reservation]
        GROUP BY [user_id]
    ) AS X
    ON X.[user_id] = U.[user_id]) as N
WHERE N.pocet is NULL

SELECT [user_name]
FROM [User]
WHERE [user_id] NOT IN (
    SELECT [user_id] 
    FROM Reservation
)
*/
-- 3  hezky vypis Reservation
SELECT u.[user_name], c.[car_name],start_time,end_time,p1.parking_name,p2.parking_name
FROM [User] as u,[Reservation]as r,Parking as p1,Parking as p2, Car as c
WHERE u.[user_id] = r.[user_id] AND r.departure_parking = p1.parking_id AND r.arrival_parking = p2.parking_id AND c.car_id = r.car_id

-- 4 
