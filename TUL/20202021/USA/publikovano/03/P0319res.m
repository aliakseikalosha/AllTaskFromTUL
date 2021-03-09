clear all

x=sym('x');
%nadefinuji distribuèní funkci bez parametru c
fx=(2-x)*(2+x);
%spoèítám integrál od -2 do 2
Fx=int(fx,-2,2)
%dopoèítám hodnotu c, tak aby distribuèní funkce dávala 1
c=1/Fx
%definuji upravenou hustotu pravdìpodobnosti
fx=c*(2-x)*(2+x);
F(x)=int(fx);
%zjištìní posunu na y ové ose - integrál pøièítá konstantu
F(x)=F(x)+0.5


x=-2:0.01:2;
y=3./32.*(2-x).*(2+x);
z=-(x.*(x.^2 - 12))/32+0.5;
hold on
plot(x,y);
plot(x,z);
title ('Graf distribuèní funkce a hustoty pravdìpodobnosti')
xlabel('namìøené hodnoty')
ylabel('hustota pravdìpodobnosti, distribuèní funkce')
hold off

x=0.3;
a=-(x.*(x.^2 - 12))/32+0.5

x=1;
b=-(x.*(x.^2 - 12))/32+0.5;
x=0;
b=b-(-(x.*(x.^2 - 12))/32+0.5)

x=1
c=1-(-(x.*(x.^2 - 12))/32+0.5)

