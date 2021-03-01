%binomické rozdìlení
%negace, že bude alespoò jeden chlapec, je, nebude ani jeden chlapec
%pravdìpodobnost, že nebude ani jeden chlapec musí být menší než 0.99

deti=1;
vysledek=1;
while vysledek>0.01
    deti=deti+1;
    vysledek=nchoosek(deti,0)*0.51^0*0.49^deti;   
end
deti