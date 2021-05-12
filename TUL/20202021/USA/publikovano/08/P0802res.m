clear all
clc

x=[2.22, 3.54, 2.37, 1.66, 4.74, 4.82, 3.21, 5.44, 3.23, 4.79, 4.85, 4.05, 3.48, 3.89, 4.90, 5.37]
rozptyl=var(x)

%H0: rozptyl je menší nebo roven 0.6
[h,p,ci,stats]=vartest(x,0.6,0.05,'right')

%H0: rozptyl je menší nebo roven 0.8086; 1; 6. Porovnejte pvalue u výsledků
[h,p,ci,stats]=vartest(x,0.8086,0.05,'right')
[h,p,ci,stats]=vartest(x,1,0.05,'right')
[h,p,ci,stats]=vartest(x,6,0.05,'right')