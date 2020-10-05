close all;clear;clc;
x = -10:0.0001:10;
y = sin(x);
plot(x,y);
hold on
z = derivace(x,y);
z(end+1) = z(end);
plot(x,z)
zz = derivace(x,z);
zz(1) = NaN;
zz(2) = NaN;
zz(end+1) = NaN ;
plot(x,zz)

[a,b] = Newton(0.8,@sin,0.00001, 0.01)
