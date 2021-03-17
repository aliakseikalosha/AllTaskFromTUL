clear all
clc

%binomické rozdìlení

%pravdìpodobnost, že bude alespoò jeden chlapec je vìtší 0.99
%znegujeme
%pravdìpodobnost, že nebudene žádný chlapec je menší 0.01

deti=1;
vysledek=1;
while vysledek>0.01
    deti=deti+1;
    vysledek=binocdf(0,deti,0.51);
end
deti
vysledek