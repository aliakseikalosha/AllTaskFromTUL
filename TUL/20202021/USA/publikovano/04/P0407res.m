%binomick� rozd�len�
%negace, �e bude alespo� jeden chlapec, je, nebude ani jeden chlapec
%pravd�podobnost, �e nebude ani jeden chlapec mus� b�t men�� ne� 0.99

deti=1;
vysledek=1;
while vysledek>0.01
    deti=deti+1;
    vysledek=nchoosek(deti,0)*0.51^0*0.49^deti;   
end
deti