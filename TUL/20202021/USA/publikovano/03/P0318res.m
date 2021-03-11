clear all
clc

syms x;
F=(x^3)/8;
%kontrola limit na okrajích intervalu
limit(F,0);
limit(F,2);

%hustota pravděpodobnosti
f=diff(F)

%střední hodnota a rozptyl
strhod=int(x.*f,0,2)
rozptyl=int((x-strhod).^2.*f,0,2)

%a)
x=0.0;
F00=(x^3)/8;
x=1;
F10=(x^3)/8;
Pa=F10-F00

%b)
x=0.5;
F05=(x^3)/8;
x=1.5;
F15=(x^3)/8;
Pb=F15-F05

%c
x=2;
F20=(x^3)/8;
Pc=(F10-F00)+(F20-F15)