clear all
clc

A=[62,54,55,60,53,58];
B=[52,56,50,49,51];

%shoda rozptylů
[H,P,CI,STATS]=vartest2(A,B,0.05)
%rozptyly jsou shodné

%shoda středních hodnot
[H,P,CI,STATS]=ttest2(A,B,0.05)