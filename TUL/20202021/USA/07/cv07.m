clear all;clc
fprintf("Uloha 601\n")
fprintf("a\n")
subplot(2,2,1)
x = unifrnd(0,1,10000,1);
histogram(x)
fprintf("b\n")
subplot(2,2,2)
for i=1:1
    x = x+unifrnd(0,1,10000,1);
end
histogram(x./2)
fprintf("c\n")
subplot(2,2,3)
for i=1:3
    x = x+unifrnd(0,1,10000,1);
end
histogram(x./5)
fprintf("d\n")
subplot(2,2,4)
for i=1:5
    x = x+unifrnd(0,1,10000,1);
end
histogram(x./10)
fprintf("Uloha 607\n")
1-normcdf(0.52,0.5,sqrt(1/12000))
fprintf("Uloha 608\n")
normcdf(4,5,sqrt(0.25))
fprintf("Uloha 610\n")
normcdf(1000,3*400,sqrt(4*400))
fprintf("Uloha 611\n")
%DX = (1 -3.5)^2/6 + (2-3.5)^2/6 + (3...
normcdf(380.5,350,sqrt(35/12*100))-normcdf(319.5,350,sqrt(35/12*100))
fprintf("Uloha 613\n")
binopdf(77,150,16/(14+16))
poisspdf(77,150*16/30)
normcdf(77.5,150*16/30,sqrt(150*16/30*(1-16/30)))-normcdf(76.5,150*16/30,sqrt(150*16/30*(1-16/30)))