clc;clear all;close all;
%function [pol] = newtonPol(x,y)
x=[-1,1,2,3];
y=[-6,-2,-3,2];
pol = zeros(1,size(x, 2));
pol(1)=y(1);
pol(2)=(y(2)-y(1))/(x(2)-x(1));
for i=3:size(pol, 2)
    for j=1:i
        pol(i)=((y(i-2)-y(i))/(x(i-2)-x(i))-(y(i-2)-y(i-1))/(x(i-2)-x(i-1)))/(x(i)-x(i-1));
    end
end
syms t pom
pom = [t,t,t,t];
for d=2:size(pol,2)
    pom(d)=prod(pom(d)-x(1:d));
end
pol=pol.*pom;
pol=expand(sum(pol));
ezplot(pol, x(1):x(end))
%end