%otevøe data uložená v souboru
x=importdata('P0305.mat');
%setøídí data od nejmenšího po nejvìtší
x=sort(x);

%vytvoøí vektor hodnot distribuèní funkce
y=0.00005:0.0001:0.99995;
plot(x,y);
title('Distribuèní funkce');
xlabel('Namìøené hodnoty');
ylabel('Distribuèní funkce');
