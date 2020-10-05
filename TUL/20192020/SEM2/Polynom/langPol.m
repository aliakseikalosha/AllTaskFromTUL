clc;clear all;close all;
%function [pol] = langPol(x,y)
x=[-1,1,2,3];
y=[-6,-2,-3,2];
lag = ones(1,size(x, 2));
syms t
cinitel = prod(t - x);
for i=1:size(lag, 2)
    lag(i)=prod(x(i)-x(x~=x(i)));
end
pol = expand(sum(y.*(cinitel./lag)));
ezplot(pol, x(1):(x(end)))
%end

