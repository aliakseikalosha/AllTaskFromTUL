clear all
clc


%otevøe data uložená v souboru
x=importdata('P0305.mat');
%setøídí data od nejmenšího po nejvìtší
x=sort(x);

%vytvoøí vektor hodnot distribuèní funkce
%vzdálenosti mezi body na y ose by mìly být 1/poètu dat
y=0.00005:0.0001:0.99995;

hold on
subplot(2,1,1)
plot(x,y);
title('Distribuèní funkce');
xlabel('Namìøené hodnoty');
ylabel('Distribuèní funkce');

%vytvoøí histogram z dat
subplot(2,1,2)
hist(x,50)
title('Histogram');
xlabel('Namìøené hodnoty');
ylabel('Poèet dat ve skupinì');