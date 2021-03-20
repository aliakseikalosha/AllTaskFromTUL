clear all
clc

%pravdepodobnostni fce P(0)=1-p   P(1)=p

syms p
%stredni hodnota
E=(1-p)*0+p*1

%rozptyl
D=(0-E)^2*(1-p)+(1-E)^2*p;
D=simplify(D)

%směrodatná odchylka
sigma=sqrt(D);
sigma=simplify(sigma)

%šikmost
a3=((0-E)^3*(1-p)+(1-E)^3*p)/sigma^3;
a3=simplify(a3)

%špičatost
a4=((0-E)^4*(1-p)+(1-E)^4*p)/sigma^4;
a4=simplify(a4)