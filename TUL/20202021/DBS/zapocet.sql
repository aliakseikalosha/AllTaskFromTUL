--Vypište názvy hospod, do nichž dodává pivo pouze pivovar Svijany.
SELECT nazev
FROM Hospoda
WHERE id_hospoda IN(
	SELECT id_hospoda
	FROM Odebira
	WHERE id_pivo IN (SELECT id_pivo FROM Pivo WHERE id_pivovar IN (SELECT id_pivovar FROM Pivovar WHERE nazev = "Svijany"))
	SELECT id_hospoda
	FROM Odebira
	WHERE id_pivo IN (SELECT id_pivo FROM Pivo WHERE id_pivovar IN (SELECT id_pivovar FROM Pivovar WHERE nazev != "Svijany"))
)
--Vypište názvy hospod, které čepují pivo od dvou a více pivovarů (nazev, pocet).
-- POZOR: Název hospody není unikátní! Alias vnořeného SELECT příkazu je v Oracle bez "AS"! 
SELECT h.nazev, x.pocet
FROM Hospoda h LEFT JOIN (
	SELECT o.id_hospoda, COUNT(id_pivovar) pocet
	FROM Odebira o, Pivo p, Pivovar v
	WHERE o.id_pivo = p.id_pivo AND p.id_pivovar = v.id_pivovar
	GROUP BY o.id_hospoda, p.id_pivovar 
	) x ON h.id_hospoda = x.id_hospoda
WHERE x.pocet > 1
--Vypište průměrné prodejni ceny piv pro hospody v Liberci (adresa LIKE '%Liberec%'), které čepují alespoň 2 různá piva (nazev, cena).
-- POZOR: Název hospody není unikátní! 
SELECT ho.nazev, AVG(o.prodejni_cena) cena
FROM Hospoda ho,Odebira o, Pivo p
WHERE ho.adresa LIKE '%Liberec%' AND ho.id_hospoda = o.id_hospoda AND o.id_pivo = p.id_pivo
GROUP BY ho.id_hospoda, ho.nazev
HAVING COUNT(ho.id_hospoda) > 1
--Vypište názvy hospod, kde čepují alespoň piva Pilsner Urquell a Gambrinus 10. 
SELECT h.nazev
FROM Hospoda h, Odebira o
WHERE h.id_hospoda = o.id_hospoda AND o.id_piva IN(
SELECT id_pivo
FROM Pivo
WHERE jmeno LIKE '%Pilsner Urquell%' OR jmeno LIKE '%Gambrinus 10%')
--Vypište jména piv (s jejich id_pivovaru, obsahem alkoholu a hořkostí), která se jmenují stejně, jsou ze stejného pivovaru, ale mají jiné množství alkoholu a/nebo jinou hořkost (jmeno, id_pivovar, alkohol, horkost). 
SELECT DISTINCT p1.jmeno, p1.id_pivovar, p1.alkohol,p1.horkost
FROM Pivo p1, pivo p2
WHERE p1.id_pivovar = p2.id_pivovar AND p1.jmeno = p2.jmeno AND ( p1.alkohol != p2.alkohol OR p1.horkost != p2.horkost)
-- Vypište jména hospod, ve kterých čepují dvě a více piv od pivovaru Svijany.
-- POZOR: Název hospody není unikátní! 
SELECT h.nazev
FROM Hospoda h
WHERE h.id_hospoda in (
	SELECT id_hospoda
	FROM Odebira o LEFT JOIN ( SELECT * FROM Pivo WHERE id_pivovar IN (SELECT id_pivovar FROM Pivovar WHERE nazev LIKE '%Svijany%')) x ON x.id_pivo = o.id_pivo
	GROUP BY id_hospoda 	  --GROUP BY id_hospoda,id_pivo
	HAVING COUNT(id_hospoda)>1--HAVING COUNT(id_pivo) > 1
)
-- Vypište názvy hospod, které neodebírají žádné pivo od pivovaru s názvem Svijany.
-- Některé hospody vůbec neodebírají pivo. 
SELECT nazev
FROM Hospoda
WHERE id_hospoda NOT IN(
	SELECT h.id_hospoda
	FROM Hospoda h LEFT JOIN Odebira o ON o.id_hospoda = h.id_hospoda
	WHERE o.id_pivo  IN (SELECT id_pivo FROM Pivo WHERE id_pivovar IN (SELECT id_pivovar FROM Pivovar WHERE nazev LIKE '%Svijany%'))
) 
--Vypište názvy pivovarů, které nedodávají žádné pivo do hospod s názvem Formanka. 
SELECT nazev
FROM Pivovar
WHERE id_pivovar NOT IN (
	SELECT o.id_pivovar
	FROM Odebira o LEFT JOIN Hospoda h on h.id_hospoda = o.id_hospoda 
	WHERE h.id_hospoda IN (SELECT id_hospoda FROM Hospoda WHERE nazev LIKE '%Formanka%')
)
--(C) Vypište názvy pivovarů, které nedodávají žádné pivo do hospod s názvem Formanka.
SELECT nazev
FROM Pivovar
WHERE id_pivovar NOT IN (
	SELECT o.id_pivovar
	FROM Odebira o, Hospoda h 
	WHERE h.id_hospoda = o.id_hospoda AND h.id_hospoda IN (SELECT id_hospoda FROM Hospoda WHERE nazev LIKE '%Formanka%')
)
--(B) Vypište jména hospod, ve kterých čepují dvě a více piv od pivovaru Svijany. POZOR: Název hospody není unikátní!
SELECT h.nazev
FROM Hospoda h
WHERE h.id_hospoda in (
	SELECT id_hospoda
	FROM Odebira o LEFT JOIN ( SELECT id_pivo FROM Pivo WHERE id_pivovar IN (SELECT id_pivovar FROM Pivovar WHERE nazev LIKE '%Svijany%')) x ON x.id_pivo = o.id_pivo
	GROUP BY id_hospoda	  
	HAVING COUNT(id_hospoda)>1
)

-- Vypište pro jednotlivé hospody pivo, z jehož prodeje má hospoda maximální zisk (hospoda, pivo, zisk).
-- POZOR: Název hospody není unikátní! Piv s maximálním ziskem může být v hospodě více! Alias vnořeného SELECT příkazu je v Oracle bez "AS"! 
SELECT X.hospoda,X.pivo,X.zisk 
FROM (

) Y INNER JOIN () X


SELECT h.nazev hospoda,p.jmeno pivo,MAX(o.prodejni_cena - o.kupni_cena) zisk 
FROM Hospoda h, Odebira o, Pivo p
WHERE h.id_hospoda = o.id_hospoda AND o.id_pivo = p.id_pivo
GROUP BY h.nazev,p.jmeno