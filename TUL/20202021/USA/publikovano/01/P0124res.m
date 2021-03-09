clear all
clc

%poèet èísel
cisel=1000-100+1;
%poèet prvoèísel mezi 100 a 1000
prvocisel=length(primes(1000))-length(primes(100));
%poèet èísel dìlitelnıch dvìma
%nutno zapoèítat i krajová èísla
dve=451;
%poèet èísel dìlitelnıch tøema
tri=300;
%poèet èísel dìlitelnıch pìti
pet=181;

%a)
%jevy mají spoleènı prùnik v šestce
%pocet èísel delitelnıch šesti
sest=150;
a=(prvocisel+dve+tri-sest)/cisel

%b)
%doplnìk k poètu prvoèísel
b=(cisel-prvocisel)/cisel

%c)
%jevy mají spoleènı prùnik v desítce
deset=91;
c=(dve+pet-deset)/cisel

