clear all
clc
hold on

subplot(2,2,1)
x1=0:0.1:20;
y1=1-exp(-x1/5);
plot(x1,y1)
title('priklad 1')
ylabel('F(x)')

subplot(2,2,2)
x1=0:0.1:20;
y1=1.2-exp(-x1/5);
plot(x1,y1)
title('priklad 2')
ylabel('F(x)')

subplot(2,2,3)
x1=0:0.1:20;
y1=1-0.5*exp(-x1/5)+0.1*sin(0.26*x1);
plot(x1,y1)
title('priklad 3')
ylabel('F(x)')

subplot(2,2,4)
x1=0:0.1:20;
y1=1-exp(-x1/5);
plot(x1-20,y1)
title('priklad 4')
ylabel('F(x)')

hold off




