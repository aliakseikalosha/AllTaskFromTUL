clear all
clc


%N=32
%n=3
%poèet es: M=4
%vybírám es: x=2

%nevracíme zpìt do balíèku
vysledek=hygepdf(2,32,4,3)

%vracíme do balíèku
%pravdìpodobnost vytažení esa: p=4/32=0.125
%vytahuji 3 karty a chci 2 esa, proto 3 nad 2

vysledek=binopdf(2,3,0.125)