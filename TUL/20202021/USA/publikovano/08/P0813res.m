clear all
clc

%otevøe data uložená v souboru
x=importdata('P0812.mat');
str_hodnota=mean(x)
rozptyl=var(x)

[p,h,stats]=signrank(x,220)
boxplot(x)
sikmost=skewness(x)
%protože data nesymetrická, je lepší znaménkový test