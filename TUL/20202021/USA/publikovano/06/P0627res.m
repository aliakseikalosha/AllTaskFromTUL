x=-5:0.01:5;

hold on
y=tpdf(x,1);
plot(x,y);
y=tpdf(x,2);
plot(x,y);
y=tpdf(x,4);
plot(x,y);
y=normpdf(x,0,1);
plot(x,y);
hold off