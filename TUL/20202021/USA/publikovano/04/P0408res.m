clear all
clc

%pravdìpodobnost poruchy je 10 %
%pravdìpodobnost OK je 90 %

%pravdìpodobnost, že je v poruše právì 0 výrobkù je
binopdf(0,20,0.1)

%pravdìpodobnost, že je v poruše 5 a ménì výrobkù
binocdf(5.5,20,0.1);
%a doplnìk k výsledku je pravdìpodobnost, že je v poruše více než 5 výrobkù
vysledek=1-binocdf(5.5,20,0.1)