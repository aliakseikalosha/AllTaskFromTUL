clear all
clc

vstup=importdata('P0327.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy  v listu1
x=vstup.List1(:,1);

%tvorba histogramu
figure;
hist(x,20)
title('plat v továrnì');
xlabel('plat v korunách');
ylabel('poèet zamìstnancù');

%výpoèet parametrù
strhodnota=mean(x)
geomprumer=geomean(x)
median_x=median(x)

%tvorba distribuèní funkce
figure;
cdfplot(x)