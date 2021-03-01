clear all
clc

%a) 
%první, druhá i tøetí kostka musí mít 6. 
%pravdìpodobnost jedné šestky je:
P1=1/6;
%pravdìpodobnost, že na první je šestka, zároveò na druhé a zároveò na
%tøetí je
Pa=(1/6)^3

%b)
%mohou padnout samé 1, 2, 3, 4, 5, nebo 6
%6 možných pøípadù
Pb=Pa*6
%nebo na prvním hodu nezáleží výsledek, druhý hod musí být stejný a tøetí
%také
Pb=1* 1/6 * 1/6

%c
%padnou dvì šestky, tøetí hod bude jiné èíslo (nesmí být stejné) - 5 pøípadù
%šestky mohou padnout buï jako první a druhá, první a tøetí, druhá a tøetí 
%3 pøípady
Pc=5*3*Pa