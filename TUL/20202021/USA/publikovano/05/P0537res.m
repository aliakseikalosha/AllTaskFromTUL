x=lognrnd(4,sqrt(5),1,100);
hold on
subplot(2,1,1),boxplot(x)
subplot(2,2,3),wblplot(x)
subplot(2,2,4),normplot(x)
hold off