clear all; clc;
%syms x a b;
%F(x) = 1 - exp( -(a*x)^b)
%invF(x) = diff(F(x))


d=importdata('P0312.mat');
x = d(:,1);
y = d(:,2);
a = 0:1/10000:1 - 1/100000;
subplot(2,2,1)
plot(a,sort(x))
subplot(2,2,2)
plot(a,x+y)

subplot(2,2,3)
plot(a,x*y)
subplot(2,2,4)
plot(a,log(x/y))