%M=32
%N=3
%poèet es: K=4
%vybírám es: x=2

%nevracíme zpìt do balíèku
vysledek=hygepdf(2,32,4,3)

%vracíme do balíèku
%pravdìpodobnost vytažení esa: p=4/32=0.125
%vytahuji 3 karty a chci 2 esa, proto 3 nad 2

vysledek=nchoosek(3,2)*0.125^2*0.875^1