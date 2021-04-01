clear all; clc
%416
%a
poisspdf(0,2)
%b
poisspdf(2,2)
%c
1-poisscdf(5,2)
%417
lamda = 5/10 * 2*25;
%a
poisscdf(11,lamda)
%b
poisspdf(20,lamda)
%c
1-poisscdf(25,lamda)
%420
%a
binocdf(520,1000,0.5) - binocdf(479,1000,0.5)
%b
poisscdf(520,1000*0.5) - poisscdf(479,1000*0.5)
%422
poisspdf(4,1.5)
floor(poisspdf(0,1.5)*1000)
%423
%a
binopdf(25,100,0.2)
%b
poisspdf(25,100*0.2)
%512
load("P0512.mat")
[a,b] = expfit(x)
%517
expcdf(2,3)
%517b
P0517b = readtable("/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/05/P0517b.xlsx");
P0517b = table2array(P0517b);
all = [P0517b',10000];
cens = [zeros(1,79),1];
freq = [ones(1,79),21];
ex = expfit(all,0.05,cens,freq)


