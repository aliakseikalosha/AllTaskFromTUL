--1.Seznam dodavatelů (jmeno, mesto), z nichž každý něco dodává.
/*
SELECT DISTINCT Dodavatel.cisdod,Dodavatel.jmeno, Dodavatel.mesto
FROM Dodavatel, Dodava
WHERE Dodavatel.cisdod  = Dodava.cisdod;

SELECT DISTINCT Dodavatel.jmeno, Dodavatel.mesto
FROM Dodavatel INNER JOIN Dodava ON (Dodavatel.cisdod = Dodava.cisdod)
 
SELECT jmeno, mesto FROM Dodavatel
WHERE cisdod IN (
    SELECT cisdod FROM Dodava
)
*/
--2.Seznam dodavatelů (jmeno, mesto), kteří nic nedodávají.
/*
SELECT jmeno, mesto FROM Dodavatel
WHERE cisdod NOT IN (
    SELECT cisdod FROM Dodava
)
*/
--3.Čísla dodavatelů, kteří dodávají součástku číslo 15.
/*
SELECT cisdod FROM Dodava WHERE cissou = 15
*/
--4.Čísla dodavatelů, kteří dodávají něco, co není součástka číslo 15.
/*
SELECT DISTINCT cisdod FROM Dodava WHERE cissou != 15
*/
--5.Čísla dodavatelů, kteří nedodávají součástku číslo 15.
/*
SELECT cisdod FROM Dodavatel
WHERE cisdod  NOT IN (
    SELECT cisdod FROM Dodava WHERE cissou = 15
)
*/
--6.Čísla dodavatelů, kteří dodávají něco i mimo součástky číslo 15.
/*
SELECT cisdod 
FROM Dodavatel
WHERE cisdod IN (
    SELECT cisdod FROM Dodava WHERE cissou = 15 
    INTERSECT
    SELECT cisdod FROM Dodava WHERE cissou != 15
)
*/
--7.Čísla dodavatelů, kteří dodávají pouze součástku číslo 15.
/*
SELECT cisdod FROM Dodavatel
WHERE cisdod  IN (
    SELECT cisdod FROM Dodava WHERE cissou = 15 
    EXCEPT
    SELECT cisdod FROM Dodava WHERE cissou != 15
)
*/
--8.Čísla dodavatelů, kteří dodávají něco, ale nedodávají součástku číslo 15.
/*
SELECT cisdod FROM Dodavatel
WHERE cisdod  IN (
    SELECT DISTINCT cisdod FROM Dodava != 15
    EXCEPT
    SELECT cisdod FROM Dodava WHERE cissou = 15
)
*/
--9.Čísla dodavatelů, kteří dodávají alespoň součástky 12, 13, 15.
/*
SELECT cisdod FROM Dodavatel
WHERE cisdod  IN (
    SELECT cisdod FROM Dodava WHERE cissou = 12
    INTERSECT
    SELECT cisdod FROM Dodava WHERE cissou = 13
    INTERSECT
    SELECT cisdod FROM Dodava WHERE cissou = 15
)
*/
--10.Čísla dodavatelů, kteří dodávají všechny dodávané součástky.
/*
SELECT cisdod, COUNT(cissou)  AS pocet FROM Dodava
GROUP BY cisdod
HAVING COUNT(cissou) =  (SELECT  COUNT(DISTINCT cissou) FROM Dodava)
*/
--11.Seznam měst, ze kterých je dodávána alespoň jedna červená součástka.
/*

*/
--12.Průměrnou cena součástky.
/*
SELECT cissou FROM Soucastka WHERE cena = (SELECT MIN(cena) FROM Soucastka)
*/
--13.Součet cen dodávaných součástek pro každého dodavatele z Liberce, který dodává alespoň 5 součástek.
/*

*/
