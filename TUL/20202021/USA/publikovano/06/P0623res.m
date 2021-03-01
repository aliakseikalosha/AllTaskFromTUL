x=0:0.05:20;

hold on
y=chi2pdf(x,2);
plot(x,y);
y=chi2pdf(x,4);
plot(x,y);
y=chi2pdf(x,6);
plot(x,y);
hold off