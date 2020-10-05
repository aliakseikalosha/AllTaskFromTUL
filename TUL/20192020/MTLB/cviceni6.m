close all;clear;clc;

syms x
disp('prvni')
figure(1);
f = atan((x+1)/(x-1));

limInf = limit(f,x,inf)
limNegInf = limit(f,x,-inf)
lim1Right =  limit(f,x,1,'right')
lim1Left = limit(f,x,1,'left')
lim1 = limit(f,x,1)
fplot(f)

figure(2);
disp('druha')
f = x/sqrt(x^2-1);

limInf = limit(f,x,inf)
limNegInf = limit(f,x,-inf)
limNeg11Left = limit(f,x,-1,'left')
lim1Rigth = limit(f,x,1,'right')
fplot(f)

figure(3);
disp('treti')
f = x^x;

limInf = limit(f,x,inf)
lim0Rigth = limit(f,x,0,'right')
fplot(f,[-1,2.2])