clear all
clc

%otevøe data uložená v souboru
x=importdata('P0815b.mat');
[par,io]=wblfit(x,0.05)

%Pokud bychom chtìli vìdìt pvalue, upravujeme hodnotu pval
