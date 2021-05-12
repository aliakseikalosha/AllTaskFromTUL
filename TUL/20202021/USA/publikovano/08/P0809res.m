clear all
clc

Spotreba=[8.8, 8.9, 9.0, 8.7, 9.3, 9.0, 8.7, 8.8, 9.4, 8.6, 8.9];

%H0: medián je 8.8, H1: medián není roven 8.8
[p,h]=signtest(Spotreba,8.8,0.05)