x=[11,8,14,5,7,5];
suma=0;
for i=1:length(x)
    suma=suma+(x(i)-50/6)^2/(50/6);
end
pvalue=1-chi2cdf(suma,length(x)-1)

hranice=chi2inv(0.95,length(x)-1);
if suma<hranice
    'H0'
else
    'H1'
end