clear all
clc


%pravdìpodobnost padnutí orla je 0.5

%pravdìpodobnost 2 úspìchy z 5 urèím z pravdìpodobnostní funkce
a=binopdf(2,5,0.5)


%chceme pravdìpodobnost, že padne 4x nebo 5x. Více možných jevù, proto
%výpoèet z distribuèní funkce. 
%Poèítám doplnìk k distribuèní funkci (stavy 0,1,2,3), protože je to snažší. 
%Lze i pøes binopdf (4,5,0.5)+binopdf(5,5,0.5)

b=1-binocdf(3.5,5,0.5)

%v pøíkazu je binocdf(3.5,... . Použil jsem, protože takto mám jistotu, že
%3 nebude do výsledku zapoèítána. Pravdìpodobnost padnutí èísla mezi 3 a 4
%(nepoèítám 3 a 4) je nulová. 