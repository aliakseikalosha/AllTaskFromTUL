clear all;clc
fprintf("Uloha 521")
load("/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/05/P0521.mat")
a = wblfit(x)
%ukazka dat
%plot(sort(x)) 

fprintf("Uloha 522")
d = -10:0.01:10,0;
x = normpdf(d,0,sqrt(1));
plot(x)
hold on
x = normpdf(d,0,sqrt(4));
plot(x)
x = normpdf(d,4,sqrt(1));
plot(x)
x = normpdf(d,4,sqrt(4));
plot(x)
hold off
fprintf("Uloha 522")
%a
norminv(.2,5,sqrt(4))
%b
norminv(.5,5,sqrt(4))
%d
normcdf(3.5,5,sqrt(4))
%e
normcdf(8,5,sqrt(4))

fprintf("Uloha 525")
normcdf(51,50,sqrt(0.49)) - normcdf(49,50,sqrt(0.49))

fprintf("Uloha 526")
fprintf("a")
p = normcdf(5,0,3)-normcdf(-2,0,3)
fprintf("b")
1-binocdf(0,3,1-p)

fprintf("Uloha 529")
fprintf("a")
a = exprnd(100,1,100);
boxplot(a)
hold on

fprintf("b")
b = wblrnd(100,1.5,100);
boxplot(b)
fprintf("c")
c = wblrnd(100,3,100);
boxplot(c)
hold off
fprintf("d")




