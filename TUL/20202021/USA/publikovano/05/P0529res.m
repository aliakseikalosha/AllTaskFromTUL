x=exprnd(100,1,100);
y=wblrnd(100,1.5,1,100);
z=wblrnd(100,3,1,100);
a=normrnd(100,30,1,100);
vysledek=[x;y;z;a]';
hold on
subplot(2,1,1),boxplot(vysledek)
subplot(2,2,3),normplot(a)
subplot(2,2,4),wblplot(a)
hold off