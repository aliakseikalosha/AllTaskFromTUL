clear all
clc

%a - právì 5 návštìv
vysledek_a=geopdf(4,0.1)

%b - ménì než 5 návštìv, tj. maximálnì 4
vysledek_b=geocdf(3.5,0.1)

%c - 8 a více, urèujeme pøes doplnìk
%doplnìk je úspìšný do 7. pokusu, tj. do 6 neúspìšných pokusù
vysledek_c=1-geocdf(6.5,0.1)