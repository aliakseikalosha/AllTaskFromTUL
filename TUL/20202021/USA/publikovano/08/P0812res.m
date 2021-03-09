%otevøe data uložená v souboru
x=importdata('P0812.mat');
str_hodnota=mean(x)
rozptyl=var(x)

[p,h,stats]=signtest(x,220)

