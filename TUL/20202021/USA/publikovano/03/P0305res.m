%otev�e data ulo�en� v souboru
x=importdata('P0305.mat');
%set��d� data od nejmen��ho po nejv�t��
x=sort(x);

%vytvo�� vektor hodnot distribu�n� funkce
y=0.00005:0.0001:0.99995;
plot(x,y);
title('Distribu�n� funkce');
xlabel('Nam��en� hodnoty');
ylabel('Distribu�n� funkce');
