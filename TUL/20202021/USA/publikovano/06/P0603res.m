%vygenerov�n� prvn�ch ��sel
A=normrnd(5,2,10000,1);
%vygenerov�n� druh�ch ��sel
B=normrnd(-5,2,10000,1);

C=A+B;
subplot(2,2,1)
hist(A,20);

subplot(2,2,2)
hist(B,20)

x=-10:0.1:10;
y=normpdf(x,0,sqrt(8));

subplot(2,2,3)
hold on
hist(C,20)
%n�sob�m 20000, proto�e m�m 20000 dat v histogramu
plot(x,20000*y,'r')
hold off
%v�sledek bude m�t norm�ln� rozd�len� s parametry suma st�edn� hodnoty a
%suma rozptylu

