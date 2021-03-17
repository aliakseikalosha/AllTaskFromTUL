clear all
clc

syms x
Fx=0.5+(atan(x/2))/pi()
lim_dol=limit(Fx,-inf)
lim_hor=limit(Fx,inf)

median=solve(Fx==0.5)
fx=diff(Fx)

%rozdeleni je symetricke, ale neexistuje stredni hodnota. 
%protože integral nelze spočítat. Taková matematická hříčka 
EX=int(x*fx,-inf,inf)
EX=simplify(EX)


x=-10:0.1:10;
Fx=0.5+(atan(x/2))/pi();
subplot(2,1,1)
plot(x,Fx)

fx=1./(2*pi*(x.^2/4 + 1));
subplot(2,1,2)
plot(x,fx)
