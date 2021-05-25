clear all
clc

rano=[98.5, 98.6, 98.7, 98.7, 98.7, 98.8, 98.9, 99.2, 99.3, 99.3];
odpoledne=[98.1,98.2, 98.3, 98.4, 98.6, 98.7, 98.8, 98.9, 99.0, 99.0];

var_rano=var(rano)
var_odpol=var(odpoledne)

%H0: rozptyly jsou shodné
%H1: rozptyly nejsou shodné
[H,P,CI,STATS]=vartest2(rano,odpoledne,0.05)