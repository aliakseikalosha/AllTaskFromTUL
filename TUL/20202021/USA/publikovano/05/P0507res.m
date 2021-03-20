clear all
clc

syms a b x 
fx=1/(b-a)
EX=int(x*fx,a,b);
EX=simplify(EX)

DX=int((x-EX)^2*fx,a,b);
DX=simplify(DX)

sigma=sqrt(DX)

sikmost=int((x-EX)^3*fx,a,b)/sigma^3

spicatost=int((x-EX)^4*fx,a,b)/sigma^4-3
