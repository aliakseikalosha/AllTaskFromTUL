/*
Film(id_film,nazev,rok, reziser)
Reviewer(id_reviewer,jmeno,vip)
Hodnoceni(id_reviewer,id_film,hodnoceni,datum)
*/
-- 1. Najdìte názvy filmù, které režíroval Steven Spielberg.
/*
SELECT nazev
FROM Film
WHERE reziser = 'Steven Spielberg'
*/
-- 2. Najdìte všechny roky filmù, které mají hodnocení 4 nebo 5 a seøaïte je vzestupnì.
/*
SELECT rok 
FROM Film
WHERE id_film in 
(
    SELECT id_film
    FROM Hodnoceni
    WHERE hodnoceni > 3 AND hodnoceni < 6
) 
ORDER BY rok 
*/
-- 3. Najdìte názvy všech filmù, které nemají hodnocení.
/*
SELECT nazev 
FROM Film
WHERE id_film NOT IN (
    SELECT id_film 
    FROM Hodnoceni
)
*/
-- 4. Nìkteøí hodnotitelé neposkytli datum svého hodnocení. Najdìte jména takových hodnotitelù.
/*
SELECT jmeno
FROM Reviewer
WHERE id_reviewer in (
    SELECT id_reviewer
    FROM Hodnoceni
    WHERE datum is NULL
)
*/
-- 5. Napište dotaz, který vrátí data o hodnocení v èitelnìjším formátu: jméno hodnotitele, název filmu, hodnocení a datum hodnocení. Setøiïte data podle jména hodnotitele, názvu filmu a nakonec podle hodnocení.
/*
SELECT jmeno as "Reviewer Name",nazev as "Film Name",hodnoceni,datum
FROM Film AS F,Reviewer AS R,Hodnoceni AS H
WHERE F.id_film = H.id_film AND R.id_reviewer = H.id_reviewer
ORDER BY jmeno,nazev,hodnoceni
*/
-- 6. Pro všechny pøípady, kdy urèitý reviewer hodnotil stejný film dvakrát a dal mu podruhé vyšší hodnocení, vrate jméno reviewera a název filmu.
/*
SELECT jmeno,nazev
FROM Hodnoceni AS H1, Hodnoceni AS H2, Film, Reviewer
WHERE H1.id_reviewer = H2.id_reviewer AND H1.hodnoceni > H2.hodnoceni AND H1.datum > H2.datum AND Film.id_film = H1.id_film AND H1.id_film = H2.id_film AND Reviewer.id_reviewer = H1.id_reviewer
*/
-- 7. Pro každý film, který má nìjaké hodnocení, najdìte nejvyšší dosažené hodnocení. Vypište název filmu a hodnocení, setøiïte podle názvu filmu.
/*
SELECT DISTINCT nazev,hodnoceni
FROM FILM as F, Hodnoceni as H
WHERE F.id_film = H.id_film AND H.hodnoceni =  
    (
        SELECT MAX(hodnoceni)
        FROM Hodnoceni as HM
        WHERE F.id_film = HM.id_film
    )
ORDER BY nazev
*/
-- 8. Pro každý film naleznìte název filmu a rozsah hodnocení, což je rozdíl mezi nejvyšším a nejnižším hodnocením. Setøiïte nejdøíve podle rozsahu od nejvyššího po nejnižší, potom podle názvu filmu.
/*
SELECT nazev,X.rozdil
FROM Film, (
        SELECT id_film, MAX(hodnoceni) - MIN(hodnoceni) as rozdil
        FROM Hodnoceni
        GROUP BY id_film 
    ) X
WHERE Film.id_film = X.id_film
ORDER BY rozdil DESC,nazev 
*/
-- 9. Najdìte rozdíl mezi prùmìrným hodnocením filmù uvedených pøed rokem 1980 a prùmìrné hodnocení filmù uvedených od roku 1980.
-- Ujistìte se, že jste spoèítali prùmìrné hodnocení nejdøíve pro každý film a pak prùmìr tìchto prùmìrù pøed rokem 1980 a po roce 1980.
/*
SELECT AVG(pred1980) - AVG(po1980)
FROM (
    SELECT AVG(hodnoceni) AS pred1980
    FROM Hodnoceni
    WHERE id_film in (SELECT id_film FROM Film WHERE rok < 1980) 
    GROUP BY id_film 
) X, (
    SELECT AVG(hodnoceni) AS po1980
    FROM Hodnoceni
    WHERE id_film in (SELECT id_film FROM Film WHERE rok >= 1980) 
    GROUP BY id_film 
) Y
*/
-- 10. Najdìte jména všech reviewerù, kteøí pøispìli tøemi a více hodnoceními. Pokuste se napsat dotaz bez použití HAVING nebo bez Count.
/*
SELECT jmeno
FROM Reviewer
WHERE id_reviewer in (
    SElECT id_reviewer
    FROM Hodnoceni
    GROUP BY id_reviewer
    HAVING COUNT(hodnoceni)>=3
) 
*/