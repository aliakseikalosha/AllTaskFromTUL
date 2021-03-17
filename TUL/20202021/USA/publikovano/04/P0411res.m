clear all
clc

%N=200
%M=10
%poèet losovanych: n=30
%uhádnul jsem: x=5

%nevracíme zpìt do balíèku - hypergeometrické rozdìlení
vysledek=hygepdf(5,200,10,30)

%vracíme do balíèku - binomické rozdìlení
%uhádneme 5 èísel, 10 èísel tipujeme, 30/200 šance na uhádnutí
vysledek=binopdf(5,10,30/200)

%odhad støední hodnoty je n*M/N=30*10/200=1.5
vysledek=0;
p=0;
for i=0:10
    pravd=hygepdf(i,200,10,30);
    if pravd>p
        vysledek=i;
        p=pravd;
    end
end
vysledek
    

