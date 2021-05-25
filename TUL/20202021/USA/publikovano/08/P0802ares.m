clear all
clc

x=[2.22, 3.54, 2.37, 1.66, 4.74, 4.82, 3.21, 5.44, 3.23, 4.79, 4.85, 4.05, 3.48, 3.89, 4.90, 5.37]
rozptyl=var(x)

%H0: rozptyl je menší nebo roven 0.6
%H1: rozptyl je větší než 0.6

s2=rozptyl
sigma2= 0.6 %porovnávaná hodnota

%vypočtu testovací statistiku
T=(s2/sigma2)*(length(x)-1)

testhodnota=chi2inv(0.95,length(x)-1)

if T<=testhodnota
    'Nezamítáme na hladině významnosti 5 % hypotézu H0'
else
    'Zamítáme na hladině významnosti 5 % hypotézu H0'
end