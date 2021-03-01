%otevøe data uložená v souboru
x=importdata('P0803.mat');
rozptyl=var(x)

[h,p,ci,stats]=vartest(x,2.25,0.05,'right')


[h,p,ci,stats]=vartest(x,2.25,'right')