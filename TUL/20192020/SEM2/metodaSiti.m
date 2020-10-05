clc;
clear all;
close all;
h=1/4;
k=0.2;
x=0:h:1;
y=0:k:0.8;
plot(x,zeros(1,length(x)),'r*');
hold on;
plot(x,ones(1,length(x)).*y(end),'r*');
plot(ones(1,length(y)).*0,y,'r*');
plot(ones(1,length(y)),y,'r*');
for i=2:length(y)-1
    plot(x(2:end-1), ones(1,length(x(2:end-1)))*y(i),'bo');
end
