clear all
clc

%otevøe data uložená v souboru
x=importdata('P0309.mat');

%vykreslí obrázky histogramù
subplot(2,2,1)
hist(x,10);
title('Histogram 10 sloupcù');
xlabel('Namìøené hodnoty');
ylabel('Poèet namìøených hodnot');
subplot(2,2,2)
hist(x,100);
title('Histogram 100 sloupcù');
xlabel('Namìøené hodnoty');
ylabel('Poèet namìøených hodnot')
subplot(2,2,3)
hist(x,500);
title('Histogram 500 sloupcù');
xlabel('Namìøené hodnoty');
ylabel('Poèet namìøených hodnot')
subplot(2,2,4)
hist(x,5000);
title('Histogram 5000 sloupcù');
xlabel('Namìøené hodnoty');
ylabel('Poèet namìøených hodnot')