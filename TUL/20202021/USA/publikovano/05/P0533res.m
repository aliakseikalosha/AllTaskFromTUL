%otevøe data uložená v souboru
x=importdata('P0533.mat');
boxplot(x)
% 5 nejvyšších dat je odlehlých, budou odstranìny
x=sort(x);
delka=length(x);
x=x(1:delka-5);
%boxplot(x)
[mu,sigma]=normfit(x)