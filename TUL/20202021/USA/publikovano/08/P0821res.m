clear all
clc
%data
leva=[2.8,2.0,3.2,1.9,2.5,2.6,1.7,4.1];
prava=[2.5,2.1,3.0,2.1,2.4,2.4,1.9,3.8];
%parovy test
rozdil=leva-prava;
[H,P,CI,STATS]=ttest(rozdil,0.05)

%dvouvýběrový test
[H,P,CI,STATS]=ttest2(leva,prava,0.05)