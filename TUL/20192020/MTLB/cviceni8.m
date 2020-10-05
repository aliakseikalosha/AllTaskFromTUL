clear all; clc;
%1
disp('Úloha 1')
disp('a')
f = @(x) 1./(2+cos(x));
delta = 0.001;
x = 0:delta:2*pi;
numInt = sum(f(x)*delta)
syms z;
anInt = vpa(int(f(z),z,0,2*pi))

disp('b')
f = @(x) x.*atan(x);
x = 0:delta:sqrt(3);
numInt = sum(f(x)*delta)

anInt = vpa(int(f(z),z,0,sqrt(3)))

disp('c')
f = @(x) sqrt(1 - sin(2.*x));
x = 0:delta:2*pi;
numInt = sum(f(x)*delta)
anInt = vpa(int(f(z),z,0,2*pi))
disp('d')
f = @(x) exp( -x.^2);
x = -10:delta:5;
numInt = sum(f(x)*delta)
anInt = vpa(int(f(z),z,-10,5))
disp('f')
x = -1000:delta:1000;
numInt = sum(f(x)*delta)
anInt = vpa(int(f(z),z,-1000,1000))

%2
disp('Úloha 2')
syms n k;
x = 1:1000;
disp('a')
f = @(x) (-1).^(x).*(1./(2.^(x-1)));
numSum = sum(f(x))
anSum = vpa(subs(symsum(f(k),1,n),1000))

disp('b')
f = @(x) 1./(x.*(x+1));
numSum = sum(f(x))
anSum = vpa(subs(symsum(f(k),1,n),1000))

disp('c')
f = @(x) (-1).^(x).*1./(x);
numSum = sum(f(x))
anSum = vpa(subs(symsum(f(k),1,n),1000))

disp('d')
f = @(x) (2.*x-1)./(2.^x);
numSum = sum(f(x))
anSum = vpa(subs(symsum(f(k),1,n),1000))

%3
disp('Úloha 3')
syms a
disp('a')
figure(1)
A = [1 7 a;a.^2 3 1-a;0 5 6];
fplot(det(A),[-10,10])
extremy = vpa(solve(diff(det(A),a)))

disp('b')
figure(2)
A = [a 8 -3.*a;1 (1-a).^2 a;3 -1 4];
fplot(det(A),[-10,10])
disp('Extremy ne existuji protože det(A) nemá kořeni')
x = vpa(solve(diff(det(A),a)))

