clear all
clc

syms x;
%nadefinuji distribuèní funkci bez parametru c
%mohu pøedpokládat že c=1 a dopoèítám ho
fx=(2-x).*(2+x);

%a)
%spoèítám integrál od -2 do 2
Fx=int(fx,-2,2)
%dopoèítám hodnotu c, tak aby distribuèní funkce dávala 1
c=1/Fx

%hustotu vynásobím c
fx=c*(2-x).*(2+x)

%distribuèní funkce
Fx=c*int(fx)
    %zjištìní posunu
posun=limit (Fx,-2)
Fx=-posun+c*int(fx)
    %posun se rovná 1/2

%b)
x=-2:0.01:2;
f=3./32.*(2-x).*(2+x);
F=-(x.*(x.^2 - 12))/32+0.5;
hold on
plot(x,f);
plot(x,F);
title ('Graf distribuèní funkce a hustoty pravdìpodobnosti')
xlabel('namìøené hodnoty')
ylabel('hustota pravdìpodobnosti, distribuèní funkce')
hold off

%c)
x=0.3;
Pa=-(x.*(x.^2 - 12))/32+0.5

x=1;
Pb1=-(x.*(x.^2 - 12))/32+0.5;
x=0;
Pb0=(-(x.*(x.^2 - 12))/32+0.5);
Pb=Pb1-Pb0

x=1;
Pc=1-(-(x.*(x.^2 - 12))/32+0.5)

