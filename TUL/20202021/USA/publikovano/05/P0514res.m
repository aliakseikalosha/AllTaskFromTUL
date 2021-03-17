clear all
clc

syms lambda x positive
fx=lambda*exp(-lambda*x);

EX=int(x*fx,0,inf);
EX=simplify(EX)

DX=int((x-EX)^2*fx,0,inf);
DX=simplify(DX)

sigma=sqrt(DX)

sikmost=int((x-EX)^3*fx,0,inf)/sigma^3

spicatost=int((x-EX)^4*fx,0,inf)/sigma^4-3
