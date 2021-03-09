clear all
pbila=20/30;
opakovani=5;
x=[0,1,0,1,0,2,0,1,1,0,0,1,0,1,0,2,0,1,1,0,0,0,0,1,2,1,1,2,1,0,1,2,1,2,3]; 

losovani=length(x);

E=[binopdf(0,5,2/3),binopdf(1,5,2/3),binopdf(2,5,2/3),binopdf(3,5,2/3),binopdf(4,5,2/3),binopdf(5,5,2/3)];
E=E.*losovani;
O=[14,14,6,1,0,0];

suma=0;
for i=1:length(E)
    suma=suma+(O(i)-E(i)).^2./E(i);
end

pvalue=1-chi2cdf(suma,length(E)-1)

hranice=chi2inv(0.95,length(E)-1);
if suma<hranice
    'H0'
else
    'H1'
end

%správnì bychom ještì mìli spojit intervaly, ale rozdíl je tak patrný, že
%to na výsledku nic nezmìní. 
