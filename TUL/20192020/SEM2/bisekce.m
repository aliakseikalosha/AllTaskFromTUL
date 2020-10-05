clc;clear all;close all;
syms x
f = x^3 + x - 1
fplot(f)
hold on
t=-5:1:5;
y=zeros(size(t));
plot(t, y)
%podle grafu zjistime interval, ne kterem lezi koren
a=-1;
b=2;
c=a;
while abs((subs(f, c))-0) > 10^-5 
    c=(a+b)/2;
    if subs(f,a)*subs(f, c)< 0 || subs(f,a)*subs(f,c)<0
        b=c;
    elseif subs(f,c)*subs(f, b)<0 || subs(f,c)*subs(f,b)<0
            a=c;
    end
end
disp("Korenem je: ")
disp(c)