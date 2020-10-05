clc;clear;
% uloha 1
figure(1)
a = [1:6,1].*pi./3;
x = cos(a);
y = sin(a);
plot(x,y)
% uloha 3
x = -5:0.05:5;
y = [functionCv5(x);functionCv5(x,1); functionCv5(x,2)];
% a
figure(2)
plot(x, y)
title('Grafy f(x), f(x,1), f(x,2)');
xlabel('x');
ylabel('y');
legend('f(x)','f(x,1)','f(x,2)');
% b
figure(3)
h(1) = subplot(3,1,1);
plot(x,y(1,:),'r');
title('y = 0');
xlabel('x');
ylabel('y');
h(2) = subplot(3,1,2);
plot(x,y(2,:),'g');
title('y = 1');
xlabel('x');
ylabel('y');
h(3) = subplot(3,1,3);
plot(x,y(3,:),'b')
title('y = 2');
xlabel('x');
ylabel('y');

linkaxes(h);

%uloha 3
figure(4)
x = -5:0.1:5;
y = -6:0.2:6;
[X,Y] =  meshgrid(x,y);
Z = functionCv5(X,Y);
mesh(X,Y,Z)