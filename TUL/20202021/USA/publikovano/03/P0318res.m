x=sym('x');
F=(x^3)/8;
f=diff(F)

strhod=int(x*f,0,2)
rozptyl=int((x-strhod)^2*f,0,2)

x=0;
x00=(x^3)/8;
x=0.5;
x05=(x^3)/8;
x=1;
x10=(x^3)/8;
x=1.5;
x15=(x^3)/8;
x=2;
x20=(x^3)/8;

a=x10-x00
b=x15-x05
c=(x10-x00)+(x20-x15)