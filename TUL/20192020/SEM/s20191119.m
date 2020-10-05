clear;clc;
x = -2:0.001:2;
y(1,:) = functionS(x);
y(2,:) = 0.*x;
plot(x,y)
%x^3 - 2*x +1 = (x-1)(x^2 + ax + b) = (x^3 + ax^2 +bx - x^2 -ax -b) = x^3
%+x^2(a)
eps = 0.000001;
BisectionMethod(-2,0,@functionS,eps)
BisectionMethod(0, 0.95,@functionS,eps)
BisectionMethod(0.95, 2,@functionS,eps)

BisectionMethod(1, 4,@functionS,eps)
