clear all

x=sym('x');
%nadefinuji distribu�n� funkci bez parametru c
fx=(2-x)*(2+x);
%spo��t�m integr�l od -2 do 2
Fx=int(fx,-2,2)
%dopo��t�m hodnotu c, tak aby distribu�n� funkce d�vala 1
c=1/Fx
%definuji upravenou hustotu pravd�podobnosti
fx=c*(2-x)*(2+x);
F(x)=int(fx);
%zji�t�n� posunu na y ov� ose - integr�l p�i��t� konstantu
F(x)=F(x)+0.5


x=-2:0.01:2;
y=3./32.*(2-x).*(2+x);
z=-(x.*(x.^2 - 12))/32+0.5;
hold on
plot(x,y);
plot(x,z);
title ('Graf distribu�n� funkce a hustoty pravd�podobnosti')
xlabel('nam��en� hodnoty')
ylabel('hustota pravd�podobnosti, distribu�n� funkce')
hold off

x=0.3;
a=-(x.*(x.^2 - 12))/32+0.5

x=1;
b=-(x.*(x.^2 - 12))/32+0.5;
x=0;
b=b-(-(x.*(x.^2 - 12))/32+0.5)

x=1
c=1-(-(x.*(x.^2 - 12))/32+0.5)

