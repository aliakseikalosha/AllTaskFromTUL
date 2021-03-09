%M=200
%N=10
%poèet losovanych: K=30
%uhádnul jsem: x=5

%nevracíme zpìt do balíèku - hypergeometrické rozdìlení
vysledek=hygepdf(5,200,30,10)

%vracíme do balíèku - binomické rozdìlení
%uhádneme 5 èísel, 10 èísel tipujeme, 30/200 šance na uhádnutí
vysledek=binopdf(5,10,30/200)

%odhad støední hodnoty je 1.5
vysledek_1=hygepdf(1,200,30,10);
vysledek_2=hygepdf(2,200,30,10);
if vysledek_1>vysledek_2
    vysledek=1
else
    vysledek=2
end
