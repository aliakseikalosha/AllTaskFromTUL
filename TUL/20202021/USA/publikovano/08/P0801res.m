clear all
clc

%otevøe data uložená v souboru
x=importdata('P0801.mat');
rozptyl=var(x)

%H0: rozptyl je menší nebo roven 2.25
%H1: rozptyl je vìtší než 2.25
[h,p,ci,stats]=vartest(x,2.25,0.05,'right')

%h=0            nezámítáme hypotézu H0, že rozptyl je menší nebo roven 2.25
%p=0.3798       pvalue je rovno 0.3798
%ci=1.8750 Inf  interval spolehlivosti je mezi 1.875 a nekoneènem
%stats:
    % chisqstat: 102.6872   testovací statistika vypoètená podle vzorce je
    %                       rovna 102.6872
    % df: 99    poèet stupòù volnosti je 99 (máme 100 namìøených dat)         


