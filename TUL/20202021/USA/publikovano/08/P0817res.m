clear all
clc

x=[3,4,5,6,8,9,9,10,11,12,13,13,14,15,15,15,16,16,17];
y=[5,5,6,6,6,7,7,8,8,9,9,10,10,11,13,15];

var(x)
var(y)
%H0: rozptyly jsou shodné
%H1: rozptyly nejsou shodné

[H,P,CI,STATS]=vartest2(x,y)