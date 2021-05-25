clear all
clc

x=[2,3,4,5,6,7,7,8,8,9,11,12,13,15,16,18,19,22,25,28,31,34,37,39,42,45,48];

%zjistím poèet hodnot menších než 3.5, první parametr v binocdf
Statistika=binocdf(2,length(x),0.25);
pvalue=2*min(Statistika, 1-Statistika)

if pvalue>0.01
    'Hypotézu H0 na hladinì významnosti 1 % nezamítám'
else
    'Hypotézu H0 na hladinì významnosti 1 % zamítám'
end

ecdf(x)