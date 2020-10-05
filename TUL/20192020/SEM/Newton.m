function [x,y] = Newton(x0,func, epsilon, delta)
y(1) = x0;
poc=2;
while abs(func(x0))>epsilon
    x1 = x0-func(x0)/(func(x0+delta)-func(x0-delta))*2*delta;
    x0 = x1;
    y(poc) = x1;
    poc = poc + 1;
end
x = x0;
end