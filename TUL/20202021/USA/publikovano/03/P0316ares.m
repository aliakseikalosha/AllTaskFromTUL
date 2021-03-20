clear all
clc

syms x positive
Fx=x*x/4;
fx=diff(Fx)
median=solve(Fx==0.5)
EX=int(fx,0,2)
P= 1.5^2/4-0.5^2/4