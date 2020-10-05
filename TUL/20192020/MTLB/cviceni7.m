clear all; clc;
%1
disp('uloha 1')
str2sym('(12/63+21/51)*7/5 - 22/7')
%2 
disp('uloha 2')
syms x y
f = log(abs(cos(x))) + y * x* exp(-x^2)
%3
disp('uloha 3')
g = subs(f, x, 1/y)
%4
disp('uloha 4')
vpa(subs(g,sym(3)),100)
%5
disp('uloha 5')
vpa(int(g,1,2),6)
%6
disp('uloha 6')
int(f,x)
%7
disp('uloha 7')
syms k n
sinsum = symsum(sin(pi*2/3*k),1,n)
NumVysl = sum(sin((1:40).*(2*pi/3)))
AnalytVysl = vpa(subs(sinsum,40))
%8
disp('uloha 8')
limit = limit(f,x,pi/2)
%9
disp('uloha 9')
int(diff(f, y),x,0,inf)
%10
disp('uloha 10')
syms a z
[x,y,z] = solve(str2sym('x-3*y+a*z=1'),str2sym('2*x-6*y+9*z=5'), str2sym('-a*x+3*y=0'));
simplify(x)
simplify(y)
simplify(z)
A = [1,-3,a;2,-6,9;-a,3,0];
p = det(A - x*eye(3))
subs(p,a,1)
roots([-1 -5 26])
%11
syms x
disp('uloha 11')
expand(str2sym('cos(3*x) - sin(3*x)'))
%12
disp(12)
solve(str2sym('exp(-x^2+4*x-9)=1'))
roots([-1,4,-9])