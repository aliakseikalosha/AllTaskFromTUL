clear all; clc;
%1
disp('Úloha 1')
figure(1)
f = @(x) sin(x)./x+exp(-(x-4).^2);
x = -10:0.1:10;
plot(x, f(x))
min24 = fminbnd(f, 2,4)
min46 = fminbnd(f, 4,6)
%2
disp('Úloha 2')
figure(2)
f = @(x) 1/4.*(x.^4)-41/6.*x.^3+209/4.*x.^2 - 135.*x;
fplot(f)
min13 = fminbnd(f,1,2.9)
%3
disp('Úloha 3')
figure(3)
f = @(x1,x2) 100.*(x2-x1.^2).^2+(1-x1).^2;
x1 =-2:0.1:2;
x2 =-2:0.1:2;
plot3(x1,x2,f(x1,x2))
f = @(x) 100.*(x(2)-x(1).^2).^2+(1-x(1)).^2;
x0 = [0,0];
minx1x2 = fminsearch(f,x0)
f = @(x) 100.*(x-(2 - x.^2).^2).^2+(1-sqrt(2-x.^2)).^2;
minx = fminbnd(f,-2,2)
%4
disp('Úloha 4')
linprog([-5,-2,-6],[1,-1,1;3,2,4;3,2,0;],[20,42,30],[],[],[0,0,0],[])
%5
disp('Úloha 5')
quadprog([8,2,0;2,4,-3;0,-3,6],[-1,0,0],[1,-1,1;1,2,-6],[-1,5],[],[],[0,0,0],[])

