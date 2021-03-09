clear all

x=sym('x');
%nadefinuji distribuční funkci bez parametru c
fx=(2-x)*(2+x);
%spočítám integrál od -2 do 2
Fx=int(fx,-2,2)
%dopočítám hodnotu c, tak aby distribuční funkce dávala 1
c=1/Fx
%definuji upravenou hustotu pravděpodobnosti
fx=c*(2-x)*(2+x);
F(x)=int(fx);
%zjištění posunu na y ové ose - integrál přičítá konstantu
F(x)=F(x)+0.5


x=-2:0.01:2;
y=3./32.*(2-x).*(2+x);
z=-(x.*(x.^2 - 12))/32+0.5;
hold on
plot(x,y);
plot(x,z);
title ('Graf distribuční funkce a hustoty pravděpodobnosti')
xlabel('naměřené hodnoty')
ylabel('hustota pravděpodobnosti, distribuční funkce')
hold off

x=0.3;
a=-(x.*(x.^2 - 12))/32+0.5

x=1;
b=-(x.*(x.^2 - 12))/32+0.5;
x=0;
b=b-(-(x.*(x.^2 - 12))/32+0.5)

x=1
c=1-(-(x.*(x.^2 - 12))/32+0.5)

