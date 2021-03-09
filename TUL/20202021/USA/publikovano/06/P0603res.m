%vygenerování prvních èísel
A=normrnd(5,2,10000,1);
%vygenerování druhých èísel
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
%násobím 20000, protože mám 20000 dat v histogramu
plot(x,20000*y,'r')
hold off
%výsledek bude mít normální rozdìlení s parametry suma støední hodnoty a
%suma rozptylu

