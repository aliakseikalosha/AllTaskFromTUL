clear all
clc

%otevøe data uložená v souboru
x=importdata('P0801.mat');
rozptyl=var(x)

%a)
%H0: rozptyl je menší nebo roven 1.875
%H1: rozptyl je vìtší než 1.875
[h,p,ci,stats]=vartest(x,1.875,0.05,'right')

%h=0            nezámítáme hypotézu H0, že rozptyl je menší nebo roven 1.875
%p=0.05         pvalue je rovno 0.05
%ci=1.8750 Inf  interval spolehlivosti je mezi 1.875 a nekoneènem
%stats:
    % chisqstat: 123.2247   testovací statistika vypoètená podle vzorce je
    %                       rovna 123.2247
    % df: 99    poèet stupòù volnosti je 99 (máme 100 namìøených dat)         

%b)
%H0: rozptyl je menší nebo roven 1.875
%H1: rozptyl je vìtší než 1.875
[h,p,ci,stats]=vartest(x,1.8,0.05,'right')

%h=1            zámítáme hypotézu H0, že rozptyl je menší nebo roven 1.80
%p=0.0252       pvalue je rovno 0.0252
%ci=1.8750 Inf  interval spolehlivosti je mezi 1.875 a nekoneènem
%stats:
    % chisqstat: 128.359    testovací statistika vypoètená podle vzorce je
    %                       rovna 128.359
    % df: 99    poèet stupòù volnosti je 99 (máme 100 namìøených dat)  

