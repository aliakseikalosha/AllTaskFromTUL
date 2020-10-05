clc;clear all;close all;
x=[-1,1,2,3]';
y=[-6,-2,-3,2];
y=y';
B=zeros(size(x,1),size(x',2)-1);
V=[B x [1;1;1;1]];
for i=1:size(V,2)-1
    V(:,end-i)=x.^i;
end
disp(V)
a=y\V;
syms t
pom=[t^4, t^3, t^2, t, 1];
P=sum(a.*pom)
ezplot(P, x(1):(x(end)))