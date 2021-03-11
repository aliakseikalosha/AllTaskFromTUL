clear all
clc

syms lambda t;

%výpoèet distribuèní funkce
y=lambda*exp(-(lambda*t));
z=int(y)

%výpoèet limity v bodì 0
limit(z,0)
%limita je rovna -1, musím pøipoèítat 1 aby byla nulová

%Distribuèní funkce
z=1+int(y)
limit(z,0)
limit(z,inf)