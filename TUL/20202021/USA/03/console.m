load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0331.mat')
ones(4,1)%hist([ones(4,1),ones(16,1)])
hist([ones(4,1),ones(16,1)])
hist([ones(4,1),ones(16,1)]')
hist([ones(4,1) ones(16,1)])
[ones(4,1) ones(16,1)*2]
[ones(1,4), ones(1,16)*2]
hist([ones(1,4), ones(1,16)*2, ones(1,25)*3, ones(1,10)*3,ones(1,5)*-1])
data = [ones(1,4), ones(1,16)*2, ones(1,25)*3, ones(1,10)*3,ones(1,5)*-1];
hist(data)
data = [ones(1,4), ones(1,16)*2, ones(1,25)*3, ones(1,10)*4,ones(1,5)*-1];
hist(data)
hist(data,5)
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0328.mat')
data(1,:)
data(2,:)
P0323(1,:)
P0323(:,2)
data = P0323(:,2);
hist(data)
hist(squize( data))
hist(squeeze(data))
hist(data')
d = importdata(data)
d = importdata('P0323.xlsx')
data =VarName1;
data =VarName1';
median(data)
var(data)
std(data)
modus(data)
mean(data)
clc
modus = mode(data)
Median = median(data)
avr = mean(data)
cdfplot(x)
hist(x)
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0328.mat')
clc;clear all
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0328.mat')
prum = mean(x)
rozptyl = var(x)
format long
rozptyl = var(x)
kvantil = norminv(0.95, 0.05)
horni_kvantil = quantile(x,0.75)
dolni_kvantil = quantile(x,0.25)
clc;clear all
load('/Users/aliakseikalosha/Documents/git/work/AllTaskFromTUL/TUL/20202021/USA/publikovano/03/P0331.mat')
