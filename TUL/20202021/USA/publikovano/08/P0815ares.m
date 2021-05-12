clear all
clc

tpor=[80,160,240,320,400,560,720,800,900,960];
%vytvorime vektor casovych zmen
t=[tpor,1000]
cens=[0,0,0,0,0,0,0,0,0,0,1]
freq=[1,1,1,1,1,1,1,1,1,1,40]

%parametry exp rozdeleni
[par,io]=expfit(t,0.05,cens,freq)
