clear all
clc

x=[12,14,16,18,19,19,21,23,25,27,31,35,39,42];
y=[15,18,21,24,27,29,32,35];
median(x)
median(y)
%testování shody rozdělení bude prováděno v kapitole 9
var(x)
var(y)

%[h,p,stats]=vartest2(x,y)
[p,h,stats]=ranksum(x,y)