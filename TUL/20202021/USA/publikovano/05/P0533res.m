%otev�e data ulo�en� v souboru
x=importdata('P0533.mat');
boxplot(x)
% 5 nejvy���ch dat je odlehl�ch, budou odstran�ny
x=sort(x);
delka=length(x);
x=x(1:delka-5);
%boxplot(x)
[mu,sigma]=normfit(x)