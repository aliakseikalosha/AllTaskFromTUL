clear all
clc

%poèet všech pøíkladù je 20
%poèet všech kombinací (šestic) je:
kombinace=nchoosek(20,6)

%b
%z 15 pøíkladù pravdìpodobnostivybere právì 4
%poèet kombinací je 
pravdepodobnost=nchoosek(15,4);
%z 5 pøíkladù statistiky vybere právì 2
statistika=nchoosek(5,2);

P=pravdepodobnost*statistika/kombinace
