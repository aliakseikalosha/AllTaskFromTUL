clear all
clc

vstup=importdata('P0517a.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1
x=vstup.List1(:,1);

a=expfit(x)

b=1-exp(-10000/expfit(x))

%c Poissonovo rozdìlení
c=poisspdf(2,8640/expfit(x))



