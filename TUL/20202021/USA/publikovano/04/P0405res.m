%pravd�podobnost jednoho prvku ur��m z pravd�podobnostn� funkce
vysledek_a=binopdf(2,5,0.5)
%je zde v�ce prvk�, proto z distribu�n� funkce. Po��t�m dopln�k k
%distribu�n� funkci (stavy 0,1,2,3), proto�e je to sna���. Lze i p�es
%binopdf (4,5,0.5)+binopdf(5,5,0.5)
vysledek_b=1-binocdf(3,5,0.5)