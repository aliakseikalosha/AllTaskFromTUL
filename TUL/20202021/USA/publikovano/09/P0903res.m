%zjistím, zda oèekávaná èetnost skupin je vìtší než 5.
E5=hygepdf(5,49,6,6)*1000;
E4=hygepdf(4,49,6,6)*1000;
E3=hygepdf(3,49,6,6)*1000;
%není vìtší než 5 u 4 a 5 uhádnutých èísel, proto spojím se skupinou 3
%uhádnutých èísel.
Ocekavany=[385,431,148,35];
Skutecny=[hygepdf(0,49,6,6),hygepdf(1,49,6,6),hygepdf(2,49,6,6),1-hygecdf(2.5,49,6,6)];
Skutecny=1000.*Skutecny;

%testovani
suma=0;
for i=1:length(Ocekavany)
    suma=suma+(Ocekavany(i)-Skutecny(i)).^2./Skutecny(i);
end

pvalue=1-chi2cdf(suma,length(Ocekavany)-1)

hranice=chi2inv(0.95,length(Ocekavany)-1);
if suma<hranice
    'H0'
else
    'H1'
end

Ocekavany=[385,431,148,35];
Skutecny=[binopdf(0,6,6/49),binopdf(1,6,6/49),binopdf(2,6,6/49),1-binocdf(2.5,6,6/49)];
Skutecny=1000.*Skutecny;

%testovani
suma=0;
for i=1:length(Ocekavany)
    suma=suma+(Ocekavany(i)-Skutecny(i)).^2./Skutecny(i);
end

pvalue=1-chi2cdf(suma,length(Ocekavany)-1)

hranice=chi2inv(0.95,length(Ocekavany)-1);
if suma<hranice
    'H0'
else
    'H1'
end